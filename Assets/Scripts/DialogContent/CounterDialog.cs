using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDialog : DialogContent
{
    public CounterDialog()
    {
        this.context = "계산대";
        this.backgroundImg = Resources.Load<Sprite>("story/Background_Store");

        Sprite[] all = Resources.LoadAll<Sprite>("story/chibok_story_Sheet_coke");
        Sprite smileImg = all[1];
        Sprite cokeImg = all[0];

        Dictionary<string, Dialogue[]> d = new Dictionary<string, Dialogue[]>();
        d.Add("main", new Dialogue[] {
            new Dialogue("주인공", cokeImg, "여기까지 데리고 온 의미가 있네~ 웬일로 네가 콜라를 다 사주냐"),
            new Dialogue("치복이", cokeImg, "당연하지, 여기까지 용쓰면서 쫄래쫄래 따라왔는데 친구로써 안사줄 수가 있겠냐?"),
            new Dialogue("치복이", cokeImg, "라고 할뻔~ 당연히 반띵해야지"),
            new Dialogue("주인공", cokeImg, "1. 그냥 내가 낼게\n2. 오케이\n3. 좋은 말 할때 네가 쏴라",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Alpha1, "주인공"),
                    new Option(KeyCode.Alpha2, "반띵"),
                    new Option(KeyCode.Alpha3, "협박")
                })
            ),
        });
        d.Add("주인공", new Dialogue[] {
            new Dialogue("치복이", cokeImg, "네가 사주는 콜라가 제일 맛있어 ㅋ",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("반띵", new Dialogue[] {
            new Dialogue("치복이", cokeImg, "쪼잔하네 나였으면 쐈다",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("협박", new Dialogue[] {
            new Dialogue("치복이", cokeImg, "지금 나한테 공갈협박하는거? ㄷㄷ 어디보자 이경우에는 형법 제 324조, 350조 가 성립해 : 강요죄나 공간죄의 수단인 협박은 사람의 의사결정을 자유를 제한하거나 의사...",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("end", new Dialogue[] { });

        this.dialogDic = d;
    }
}
