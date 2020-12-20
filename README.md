# UCinDungeon_Scripts

언리얼에 이어 유니티를 공부하며 컴포넌트 패턴을 주축으로 사용하고,\
포함관계가 아닌 객체들 델리게이트로 핸들링 하는 방식으로 설계하였다.

\
\
\
\
\
\
\
\
\
![image](https://user-images.githubusercontent.com/49256779/102712176-56c9e680-4302-11eb-91e7-3413de1a16a9.png)
GameBoard[Z][X]에 오브젝트의 위치 z,x값으로 접근하는 방법으로 구현\
위치값의 자료형은 float 이지만 정수 단위로만 이동시키며 구현하였는데,\
종종 위치 판정이 잘못되었고 vector3에서 x y z 값을 가져올때 부동소수점 오차가 발생하는것을 확인하여 반올림 하여 인티저로 사용하도록 구현함.\

make by Unity
