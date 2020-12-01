//Prototype : 
//AutoBlinkforSD.cs
//SDユニティちゃん用オート目パチスクリプト
//2014/12/10 N.Kobayashi
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityChan
{

    public class UDEyeBlink : MonoBehaviour
    {
        public bool isActive = true;                //オート目パチ有効
        public SkinnedMeshRenderer faceSkinMesh;    //_faceSkinMeshへの参照
        [Header("Shape")]
        public int blinkShapeIndex = 0;           //目パチ用モーフのindex
        public int[] disableShapeIndexs = { 1, 15 };

        [HideInInspector]
        public float openWeight = 0.0f;
        [Header("ShapeWeight")]
        public float halfCloseWeight = 20.0f;       //半閉じ目ブレンドシェイプ比率
        public float closeWeight = 85.0f;           //閉じ目ブレンドシェイプ比率

        [Header("Time")]
        public float blinkTime = 0.9f;              //目パチの時間
        public float interval = 3.0f;               // ランダム判定のインターバル

        private float deltaTime = 0.0f;          //タイマー残り時間
        private const int BLINK_PHASE_COUNT = 4;

        private bool canBlink = false;
        enum Status
        {
            READY,
            HALF_DOWN,
            CLOSE,
            HALF_UP,
            OPEN,
            MAX,
        }
        private Status eyeStatus;   //現在の目パチステータス

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {

            // ランダム判定用関数をスタートする
            StartCoroutine("BlinkTimer");
        }


        // Update is called once per frame
        void LateUpdate()
        {


            if ( isActive)
            {

                 deltaTime += Time.deltaTime;
                switch ( eyeStatus)
                {
                    case Status.READY:
                        {
                            SetOpenEyes();
                            TryNextPhase();
                        }
                        break;
                    case Status.CLOSE:
                        {
                            SetCloseEyes();
                            TryNextPhase();

                        }
                        break;
                    case Status.HALF_UP:
                    case Status.HALF_DOWN:
                        {
                            SetHalfCloseEyes();
                            TryNextPhase();
                        }
                        break;
                    case Status.OPEN:
                        break;
                    default:
                        break;
                }

            }
        }

        //タイマーリセット
        private void ResetTimer()
        {
             deltaTime = 0;

        }

        private void IncrementStatus(ref Status src)
        {
            if ((int)src < (int)Status.MAX)
            {
                src++;
            }
        }

        private void TryNextPhase()
        {
            if ( blinkTime / BLINK_PHASE_COUNT <  deltaTime)
            {
                IncrementStatus(ref  eyeStatus);
                ResetTimer();
            }
        }

        private void SetCloseEyes()
        {
             faceSkinMesh.SetBlendShapeWeight( blinkShapeIndex,  closeWeight);
        }

        private void SetHalfCloseEyes()
        {
             faceSkinMesh.SetBlendShapeWeight( blinkShapeIndex,  halfCloseWeight);

        }

        private void SetOpenEyes()
        {
             faceSkinMesh.SetBlendShapeWeight( blinkShapeIndex,  openWeight);

        }

        // ランダム判定用関数
        IEnumerator BlinkTimer()
        {
            // 無限ループ開始
            while (true)
            {
                 canBlink = true;
                for (int i = 0; i <  disableShapeIndexs.Length; i++)
                {
                    if ( faceSkinMesh.GetBlendShapeWeight( disableShapeIndexs[i]) != 0f)
                    {
                         canBlink = false;
                    }
                }
                if ( canBlink)
                {
                    ResetTimer();
                     eyeStatus = Status.READY;
                }
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
