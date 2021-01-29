using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveStoreDialog : DialogContent
{
    public ArriveStoreDialog()
    {
        this.context = "편의점에 막 도착한 치복이와 주인공";
        this.backgroundImg = Resources.Load<Sprite>("");

        Sprite[] all = Resources.LoadAll<Sprite>("story/chibok_story_Sheet_coke");
        Sprite smileImg = all[1];

        Dictionary<string, Dialogue[]> d = new Dictionary<string, Dialogue[]>();
        d.Add("main", new Dialogue[] {
            new Dialogue("주인공", smileImg, "하아 하아 드디어 도착했네"),
            new Dialogue("치복이", smileImg, "편의점 오는데 얼마나 걸린다고 그렇게 헐떡거려? 좀 떨어져! 땀내나!"),
            new Dialogue("주인공", smileImg, "아... 나 진짜로 죽을 뻔 했다고..."),
            new Dialogue("치복이", smileImg, "저렇게 귀여운 얘들이 뭐가 위험하다고 편의점까지 쫓아와.. 혹시 너 나 좋아하냐?"),
            new Dialogue("주인공", smileImg, "1. 당근이지 우린 불알친구잖아?\n2. 응, 너를 처음 만난 순간 나는 너의 숨결이라는 늪에 빠져버렸는걸?\n3. 무슨 헛소리를 그렇게 맛깔나게 하냐?",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Alpha1, "불알친구"),
                    new Option(KeyCode.Alpha2, "너의 숨결이라는 늪 빠짐"),
                    new Option(KeyCode.Alpha3, "헛소리")
                })
            ),
        });
        d.Add("불알친구", new Dialogue[] {
            new Dialogue("치복이", smileImg, "너 있었? 몰랐네ㅋ",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("너의 숨결이라는 늪 빠짐", new Dialogue[] {
            new Dialogue("치복이", smileImg, "어디보자.. 니 여친 전번이 010-7761-3...",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("헛소리", new Dialogue[] {
            new Dialogue("치복이", smileImg, "야~ 너 사회생활 못하겠다. 내가 지구를 구할 용사라니까? 혹시 알아? 좀 잘 보이면 떡고물 하나 떨어질 지?",
                new List<Option> (new Option[] {
                    new Option(KeyCode.Space, "end"),
                }))
        });
        d.Add("end", new Dialogue[] { });

        this.dialogDic = d;
    }
}
