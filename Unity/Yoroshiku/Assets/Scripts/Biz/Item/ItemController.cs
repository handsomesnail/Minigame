using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using Biz.Player;
using Biz.Storage;
using UnityEngine;
using ZCore;

namespace Biz.Item {
    public class ItemController : Controller<ItemModel, ItemView> {

        public void OnInitCommand(InitCommand cmd) {
            Model.Items = new List<string>(cmd.Items);
        }

        public void OnCollectCommand(CollectCommand cmd) {
            //Call (new Biz.Gaming.PauseCommand ());
            //View.ItemText.text = cmd.Item.Text;
            //数据更新
            foreach (string s in Model.Items) {
                if (s == cmd.Item.ItemName) return;
            }
            Model.Items.Add(cmd.Item.ItemName);
            // 显示ItemTip
            Call(new ShowItemTipCommand(cmd.Item));
            //创建Item特效
            GameObject effect = GameObject.Instantiate(cmd.Item.ItemCollectEffect, cmd.Item.transform.parent);
            effect.transform.position = cmd.Item.transform.position;
            SpriteRenderer renderer = effect.GetComponent<SpriteRenderer>();
            renderer.color = cmd.Item.EffectColor;
            Animator anim = effect.GetComponent<Animator>();
            anim.Play("Collection");
            AnimKiller killer = effect.AddComponent<AnimKiller>();
            killer.StateName = "Collection";
            //触发音效
            AudioSource audio = cmd.Item.Audio;
            if (!audio.isPlaying) {
                audio.Play();
            }
        }

        public List<string> OnListCollectedCommand(ListCollectedCommand cmd) {
            List<string> ret = new List<string> ();
            ret.AddRange (Model.Items);
            return ret;
        }

    }
}