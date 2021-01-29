using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveHomeDialog : DialogContent
{
    public ArriveHomeDialog()
    {
        this.context = "주인공이 치복이 집에 도착한 장면";
        this.backgroundImg = Resources.Load<Sprite>("story/Background_ChibokHouse");

        Sprite[] all = Resources.LoadAll<Sprite>("story/chibok_story_Sheet_coke");
        Sprite smileImg = all[1];
        Sprite cokeImg = all[0];

        Dictionary<string, Dialogue[]> d = new Dictionary<string, Dialogue[]>();
        d.Add("main", new Dialogue[] {
            new Dialogue("주인공", smileImg, "치복아 치복아! 너 각성했다며!"),
            new Dialogue("치복이", smileImg, "당연하지, 네가 됐는데, 내가 안될리가 없잖아?"),
            new Dialogue("주인공", smileImg, "그나저나, 퍼거슨 예언가님 이 세상을 구할 용사를 점찍었어!"),
            new Dialogue("치복이", smileImg, "퍽이나 잘 찍겠네. 누군데?"),
            new Dialogue("주인공", smileImg, "너라고 너! 말 좀 똑바로 들어! 그리고 뉴스에서 하루 죙일 니 이름밖에 안나오잖아! "),
            new Dialogue("치복이", smileImg, "동명이인일 수 도 있잖아! 니가 어케앎?"),
            new Dialogue("주인공", smileImg, "어떻게 사람 이름이 엄치복?"),
            new Dialogue("치복이", smileImg, "쩦... 아니 나인걸 어떻게 알았데... 섭섭하네... ㅇㅅㅇ"),
            new Dialogue("주인공", smileImg, "(우욱 10)"),
        });

        this.dialogDic = d;
    }
}
