using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetBuyCokeDialog : DialogContent
{
    public LetBuyCokeDialog()
    {
        this.context = "콜라사러 가자고 설득하는 장면";
        this.backgroundImg = Resources.Load<Sprite>("");

        Sprite[] all = Resources.LoadAll<Sprite>("story/chibok_story_Sheet_coke");
        Sprite smileImg = all[1];
        Sprite cokeImg = all[0];

        Dictionary<string, Dialogue[]> d = new Dictionary<string, Dialogue[]>();
        d.Add("main", new Dialogue[] {
            new Dialogue("치복이", smileImg, "그나저나 니 얼굴보니까 갑자기 시원한 콜라가 땡긴다."),
            new Dialogue("주인공", smileImg, "내가 왜?"),
            new Dialogue("치복이", smileImg, "아니.. 그냥 ㅋ 니 얼굴보니까 고구마가 연상되어서 목이 턱턱 막히네."),
            new Dialogue("주인공", smileImg, "(킹받네...)"),
            new Dialogue("치복이", smileImg, "야 말나온 김에 앞에 CV 편의점에 콜라 한 접시?"),
            new Dialogue("주인공", smileImg, "뭔솔이야? 밖에는 맵에 있는 몬스터 소환 스팟에서 3개 크기의 랜덤한 몬스터가 출몰한다는 걸 모른단 말이야?"),
            new Dialogue("주인공", smileImg, "또한, 중간크기 몬스터와 큰 몬스터는 각각 패턴을 가지고 공격하므로 잘 보고 판단하세요 여러분!"),
            new Dialogue("치복이", smileImg, "(뭔솔이야...)"),
            new Dialogue("치복이", smileImg, "네 ~~ 먼저 가드렸습니다. ㅂㅂ"),
            new Dialogue("주인공", smileImg, "(무친련 무친련) 아... 거 참 마음에 안드네."),
            new Dialogue("주인공", smileImg, "(쫓아 나가는 주인공) 몬스터 한 마리도 못잡는게... 진짜 사람 귀찮게 하네.")
        });

        this.dialogDic = d;
    }
}
