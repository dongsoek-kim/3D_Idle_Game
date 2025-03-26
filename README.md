#3D_IDLE_Game
----
3D로 구현한 방치형 게임 프레임입니다

#프로젝트 소개
-----------
개인프로젝트입니다. Unity로 개발한 3D 방치형 게임입니다.
![image](https://github.com/user-attachments/assets/ddb8f4be-ced8-484e-967e-a4d01a4dd34c)


#개발기간
----
2025.03.20~2025.03.26

#개발환경
----
Unity

#프로젝트 시작
-----
빌드되지 않았습니다. MainBrunch에서 DungeonScenes에서 구동됩니다

#외부 리소스
------
Procedural Coins
Dog Knight PBR Polyart
Level 1 Monster Pack
Buttons Set
Low Poly Medieval Blacksmith Props

#키입력
----
마우스를 클릭하여 스텟업

#주요기능
----
>navMash를 이용한 자동이동   
>![image](https://github.com/user-attachments/assets/ceb7aa9f-7050-4d0e-a3bc-bfdef7a4f8b0)

>다음 맵에 도착시 맵생성 및 이전 맵 파괴 맵은 큐에 저장하여 관리   
>![Image](https://github.com/user-attachments/assets/d60f9ff1-5548-4d2d-98a4-b0783a6089bb)

>BigInt   
>![image](https://github.com/user-attachments/assets/2bb9b253-778c-4199-9078-c60251d8bed9)   
>큰수 처리를 위한 BigInt 1000단위로 끊어 A,B,C...순으로 바뀐다

스테이지 구성
----
일반몬스터를 잡으면 게이지가 증가해 보스도전을 할수있고 보스를 잡으면 스테이지를 넘어간다.
스테이지가 넘어갈때마다 몬스터가 강력해진다. 

Issue
----------
숫자가 일정이상 커지면 망가집니다.
double이나 더 큰 숫자로 감싸줘야되는데 flaot를 써서 생긴 이슈같습니다.
