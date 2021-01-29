using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #2
public class ChoosingCokeDialog : DialogContent
{
    public ChoosingCokeDialog()
    {
        this.context = "코카콜라 고르는 장면";
        this.backgroundImg = Resources.Load<Sprite>("story/Background_Store");

        Sprite[] all = Resources.LoadAll<Sprite>("story/chibok_story_Sheet_coke");
        Sprite smileImg = all[1];
        Sprite cokeImg = all[0];

        Dictionary<string, Dialogue[]> d = new Dictionary<string, Dialogue[]>();
        d.Add("main", new Dialogue[] {
            new Dialogue("치복이", smileImg, "아~ 콜라마렵다. 니 무슨 콜라 마시고 싶냐?"),
            new Dialogue("주인공", smileImg, "1. 코카콜라지~\n2. 펩시가 맛있지 않아?",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Alpha1, "코카콜라"),
                    new Option(KeyCode.Alpha2, "펩시")
                })
            ),
        });
        d.Add("코카콜라", new Dialogue[] {
            new Dialogue("치복이", cokeImg, "어 펩시 1+1 이네. 계산대에 좀 갖다놔",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("펩시", new Dialogue[] {
            new Dialogue("치복이", cokeImg, "펩시라니~ 맛알못이네~. 이거 계산대에 좀 갖다놔",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("end", new Dialogue[] { });

        this.dialogDic = d;
    }
}
