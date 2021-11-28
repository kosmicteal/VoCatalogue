using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//Diagnostics is only used on debugging
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace VoCatalogue
{
    public enum CategoryType { VOCALOID, CeVIO, SYNTHV, AITALK, OTHERS };

    public class VoiceBank
    {
        public string Reference { get; set; }
        [JsonIgnore] public string Romanji { get; set; }
        [JsonIgnore] public string Profile { get; set; }
        public string Link { get; set; }
        public string ActivationCode { get; set; } = "";
        public bool ActivationBool { get; set; }
        [JsonIgnore] public string Website { get; set; }
        [JsonIgnore] public string GridRowPos { get; set; }
        [JsonIgnore] public string GridColPos { get; set; }
        [JsonIgnore] public CategoryType Category { get; set; }
    }

    public static class ProductData
    {

        private static string jsonString;

        //data implemented as a List, can't implement it as a Map with Reference as a Key due to WPF's limitations
        //    (maybe should be implemented as a KeyedCollection? need to check in the future if it's more dynamic)
        //
        //currently any new bank should be added right after the OTHERS section
        private static List<VoiceBank> digitalBoxes = new List<VoiceBank>()
        {
            //
            //  VOCALOID
            //
            //VOCALOID1
            new VoiceBank(){Reference="LEON", Profile="img/profile/v1/leon.jpg",Website="https://zero-g.co.uk/collections/vocaloid", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="LOLA", Profile="img/profile/v1/lola.jpg",Website="https://zero-g.co.uk/collections/vocaloid", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MIRIAM", Profile="img/profile/v1/miriam.jpg", Website="https://zero-g.co.uk/collections/vocaloid", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MEIKO", Profile="img/profile/v1/meiko.jpg", Website="https://ec.crypton.co.jp/product/detail/25220", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="KAITO", Profile="img/profile/v1/kaito.jpg", Website="https://ec.crypton.co.jp/product/detail/27720", Category=CategoryType.VOCALOID},
            //VOCALOID2
            new VoiceBank(){Reference="Sweet Ann", Profile="img/profile/v2/sweetann.jpg", Website="https://powerfx.com/products/sweet-ann", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V2]", Romanji="Hatsune Miku [V2]", Profile="img/profile/v2/miku.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/cv01", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鏡音リン・レン [V2 act1]", Romanji="Kagamine Rin & Len [V2 act1]", Profile="img/profile/v2/rinlen_act1.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Prima", Profile="img/profile/v2/prima.jpg", Website="https://zero-g.co.uk/collections/vocaloid/products/vocaloid2-prima", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鏡音リン・レン [V2 act2]", Romanji="Kagamine Rin & Len [V2 act2]", Profile="img/profile/v2/rinlen_act2.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/cv02", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V2]", Romanji="Gackpoid [V2]", Profile="img/profile/v2/gackpo.jpg", Website="https://www.ssw.co.jp/products/vocal/gackpoid/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="巡音ルカ [V2]", Romanji="Megurine Luka [V2]", Profile="img/profile/v2/luka.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/cv03", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V2]", Profile="img/profile/v2/gumi.jpg", Website="http://www.ssw.co.jp/products/vocal/megpoid/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="SONiKA", Profile="img/profile/v2/sonika.jpg", Website="https://zero-g.co.uk/products/sonika-vocaloid-2", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="SF-A2 miki [V2]", Profile="img/profile/v2/sfa2miki.jpg", Website="https://www.ah-soft.com/vocaloid/miki_v2/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="歌愛ユキ [V2]", Romanji="Kaai Yuki [V2]", Profile="img/profile/v2/yuki.jpg", Website="https://www.ah-soft.com/vocaloid/yuki_v2/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="氷山キヨテル [V2]", Romanji="Hiyama Kiyoteru [V2]", Profile="img/profile/v2/kiyoteru.jpg", Website="https://www.ah-soft.com/vocaloid/kiyoteru_v2/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="BIG AL", Profile="img/profile/v2/bigal.jpg", Website="https://powerfx.com/products/big-al", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V2 Append]", Romanji="Hatsune Miku [V2 Append]", Profile="img/profile/v2/miku_append.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/cv01a", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Tonio", Profile="img/profile/v2/tonio.jpg", Website="https://zero-g.co.uk/collections/vocaloid/products/tonio-vocaloid-2", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Lily [V2]", Profile="img/profile/v2/lily.jpg", Website="http://www.ssw.co.jp/products/vocal/lily/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY1", Profile="img/profile/v2/vy1.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="ガチャッポイド [V2]", Romanji="Gachapoid [V2]", Profile="img/profile/v2/gachapoid.jpg", Website="http://www.ssw.co.jp/products/vocal/gachapoid/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="猫村いろは [V2]", Romanji="Nekomura Iroha [V2]", Profile="img/profile/v2/iroha.jpg", Website="https://www.ah-soft.com/vocaloid/iroha_v2/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="歌手音ピコ", Romanji="Utatane Piko", Profile="img/profile/v2/piko.jpg", Website="http://www.kioon.com/vocaloid/piko/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鏡音リン・レン [V2 Append]", Romanji="Kagamine Rin & Len [V2 Append]", Profile="img/profile/v2/rinlen_append.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/cv02a", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY2", Profile="img/profile/v2/vy2.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            //VOCALOID3
            new VoiceBank(){Reference="VOCALOID3 editor", Profile="img/profile/v3/editor.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VOCALOID3 Cubase", Profile="img/profile/v3/editor_cubase.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VOCALOID3 Cubase NEO", Profile="img/profile/v3/editor_cubase_neo.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VocaListener [V3]", Profile="", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY1V3", Profile="img/profile/v3/vy1.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Mew", Profile="img/profile/v3/mew.jpg", Website="https://www.vocaloid.com/en/products/show/v3l_mew_en", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 Adult]", Profile="img/profile/v3/gumi_adult.jpg", Website="https://www.ssw.co.jp/products/vocal3/megpoid/products/adult/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 Power]", Profile="img/profile/v3/gumi_power.jpg", Website="https://www.ssw.co.jp/products/vocal3/megpoid/products/power/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 Whisper]", Profile="img/profile/v3/gumi_whisper.jpg", Website="https://www.ssw.co.jp/products/vocal3/megpoid/products/whisper/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 Sweet]", Profile="img/profile/v3/gumi_sweet.jpg", Website="https://www.ssw.co.jp/products/vocal3/megpoid/products/sweet/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="SeeU", Profile="img/profile/v3/seeu.jpg", Website="http://characterplanet.net/shop/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="兎眠りおん [V3]", Profile="img/profile/v3/rion.jpg", Website="https://sonicwire.com/product/99232", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="OLIVER", Profile="img/profile/v3/oliver.jpg", Website="https://powerfx.com/products/989250534", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="CUL", Profile="img/profile/v3/cul.jpg", Website="https://www.ssw.co.jp/products/vocal3/cul/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="結月ゆかり [V3]", Profile="img/profile/v3/yuzuki.jpg", Website="https://www.ah-soft.com/vocaloid/yukari_v3/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Bruno", Profile="img/profile/v3/bruno_v3_std.jpg", Website="https://www.voctro-vocaloid.com/brunoclara", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Clara", Profile="img/profile/v3/clara_v3_std.jpg", Website="https://www.voctro-vocaloid.com/brunoclara", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="IA [V3]", Profile="img/profile/v3/ia-aria.jpg", Website="https://ia-aria.com/cat_software/vocaloid/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 Native]", Profile="img/profile/v3/gumi_native.jpg", Website="https://www.ssw.co.jp/products/vocal3/megpoid/products/native/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="蒼姫ラピス", Profile="img/profile/v3/lapis.jpg", Website="https://www.vocaloid.com/en/products/show/v3l_aoki_lapis_en", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Lily [V3]", Profile="img/profile/v3/lily.jpg", Website="http://www.ssw.co.jp/products/vocal3/lily/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="洛天依 [V3]", Profile="img/profile/v3/luo.jpg", Website="http://thstars.com/vsinger/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V3 Native]", Profile="img/profile/v3/gackpoid_native.jpg", Website="https://www.ssw.co.jp/products/vocal3/gackpoid/products/native/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V3 Whisper]", Profile="img/profile/v3/gackpoid_whisper.jpg", Website="https://www.ssw.co.jp/products/vocal3/gackpoid/products/whisper/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V3 Power]", Profile="img/profile/v3/gackpoid_power.jpg", Website="https://www.ssw.co.jp/products/vocal3/gackpoid/products/power/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY2V3", Profile="img/profile/v3/vy2.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MAYU", Profile="img/profile/v3/mayu.jpg", Website="http://mayusan.jp/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Avanna", Profile="img/profile/v3/avanna.jpg", Website="https://zero-g.co.uk/collections/vocaloid/products/avanna-vocaloid-3", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="KAITO [V3]", Profile="img/profile/v3/kaito.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/kaitov3", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V3 English]", Profile="img/profile/v3/gumi_english.jpg", Website="http://www.ssw.co.jp/products/vocal3/megpoid/products/english/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="ZOLA Project", Profile="img/profile/v3/zola.jpg", Website="http://www.vocaloid.com/products", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="言和 [V3]", Profile="img/profile/v3/yanhe.jpg", Website="http://thstars.com/vsinger/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V3 English]", Profile="img/profile/v3/miku_english.jpg", Website="https://powerfx.com/products/sweet-ann", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="YOHIOloid", Profile="img/profile/v3/yohioloid.jpg", Website="https://powerfx.com/products/sweet-ann", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V3]", Profile="img/profile/v3/miku.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/mikuv3_bundle", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MAIKA", Profile="img/profile/v3/maika.jpg", Website="https://www.voctro-vocaloid.com/maika", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="メルリ", Profile="img/profile/v3/merli.jpg", Website="https://www.vocaloid.com/products/show/v3l_merli", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="マクネナナ [V3 JPN]", Profile="img/profile/v3/nana_jpn.jpg", Website="https://www.ah-soft.com/vocaloid/macnenana/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="マクネナナ [V3 ENG]", Profile="img/profile/v3/nana_eng.jpg", Website="https://www.ah-soft.com/vocaloid/macnenana/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MEIKO [V3]", Profile="img/profile/v3/meiko.jpg", Website="https://ec.crypton.co.jp/pages/prod/virtualsinger/meikov3", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="kokone", Profile="img/profile/v3/kokone.jpg", Website="https://www.ssw.co.jp/products/vocal3/kokone/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="anon & kanon", Profile="img/profile/v3/anokanon.jpg", Website="https://www.vocaloid.com/products/show/v3l_anonkanon", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="v flower", Profile="img/profile/v3/flower.jpg", Website="http://www.v-flower.jp/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="東北ずん子 [V3]", Profile="img/profile/v3/zunko.jpg", Website="https://www.ah-soft.com/vocaloid/zunko/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="IA [V3 ROCKS]", Profile="img/profile/v3/ia_rocks.jpg", Website="https://ia-aria.com/cat_software/vocaloid/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="galaco [V3 Prize]", Profile="img/profile/v3/galaco_prize.jpg", Website="http://www.vocaloid.com/galaco/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="galaco [V3 NEO]", Profile="img/profile/v3/galaco_neo.jpg", Website="http://www.vocaloid.com/galaco/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Rana [V3]", Profile="img/profile/v3/rana.jpg", Website="https://www.vocaloid.com/products/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="ガチャッポイド [V3]", Profile="img/profile/v3/gachapoid.jpg", Website="https://www.ssw.co.jp/products/vocal3/gachapoid/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Chika", Profile="img/profile/v3/chika.jpg", Website="https://www.ssw.co.jp/products/vocal3/chika/index.html", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="心華 [V3]", Profile="img/profile/v3/xinhua.jpg", Website="https://www.vocaloid.com/products/", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="乐正绫 [V3]", Profile="img/profile/v3/ling.jpg", Website="http://thstars.com/vsinger/", Category=CategoryType.VOCALOID},
            //VOCALOID4
            new VoiceBank(){Reference="VOCALOID4 editor", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VOCALOID4 Cubase", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VocaListener [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY1V4",Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="CYBER DIVA", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="結月ゆかり [V4 Jun]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="結月ゆかり [V4 Onn]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="結月ゆかり [V4 Lin]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="巡音ルカ [V4x]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V4 Native]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V4 Whisper]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="がくっぽいど [V4 Power]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="SF-A2 miki [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="猫村いろは [V4 Natural]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="猫村いろは [V4 Soft]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="v4 flower", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Sachiko", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="ARSLOID", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="RUBY", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="歌愛ユキ [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="氷山キヨテル [V4 Natural]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="氷山キヨテル [V4 Rock]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V4 Native]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V4 Adult]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V4 Power]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V4 Sweet]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="GUMI [V4 Whisper]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="DEX", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="DAINA", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Rana [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鏡音リン・レン [V4x]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鏡音リン・レン [V4 English]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Unity-chan", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Fukase", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="星尘 [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="音街ウナ [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V4x]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="初音ミク [V4 English]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="東北ずん子 [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="CYBER SONGMAN", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="マクネナナ [V4 Natural]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="マクネナナ [V4 Petit]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="マクネナナ [V4 English]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="UNI", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="兎眠りおん [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="夢眠ネム", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="乐正龙牙", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="AZUKI", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="MATCHA", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="LUMi", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="心華 [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="心華 [V4 JPN]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="洛天依 [V4 CHN]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="洛天依 [V4 JPN]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="紲星あかり [V4]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="ミライ小町", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="徵羽摩柯", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="墨清弦", Profile="", Website="", Category=CategoryType.VOCALOID},
            //VOCALOID5
            new VoiceBank(){Reference="VOCALOID5 editor", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="Standard Vocals [V5]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="CYBER DIVA II", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="CYBER SONGMAN II", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY1V5", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="VY2V5", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="桜乃そら [V5 Natural]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="桜乃そら [V5 Cool]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鳴花ヒメ [V5]", Profile="", Website="", Category=CategoryType.VOCALOID},
            new VoiceBank(){Reference="鳴花ミコト [V5]", Profile="", Website="", Category=CategoryType.VOCALOID},
            //
            //  CEVIO
            //
            //Creative Studio 7
            new VoiceBank(){Reference="さとうささら [CS7 Talk]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="すずきつづみ [CS7]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="タカハシ [CS7]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="IA [CS7 Talk]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="ONE [CS7 Talk]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="さとうささら [CS7 Song]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="IA [CS7 ENGLISH C Song]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="赤咲 湊", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="緑咲 香澄", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="銀咲 大和", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="金咲 小春", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="白咲 優大", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="黄咲 愛里", Profile="", Website="", Category=CategoryType.CeVIO},
            //CeVIO AI
            new VoiceBank(){Reference="結月ゆかり [C-AI Rei]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="東北きりたん [C-AI]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="IA [C-AI Talk]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="IA [C-AI Song]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="IA [C-AI Song English]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="ONE [C-AI Talk]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="ONE [C-AI Song]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="小春六花 [C-AI]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="弦巻マキ [C-AI]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="可不 [C-AI]", Profile="", Website="", Category=CategoryType.CeVIO},
            new VoiceBank(){Reference="flower [C-AI]", Profile="", Website="", Category=CategoryType.CeVIO},
            //
            // SYNTHV
            //
            //R1
            new VoiceBank(){Reference="Eleanor Forte [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},
            new VoiceBank(){Reference="闇音レンリ [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},
            new VoiceBank(){Reference="ゲンブ [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},     
            new VoiceBank(){Reference="AiKO [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},      
            new VoiceBank(){Reference="赤羽 [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},         
            new VoiceBank(){Reference="诗岸 [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="苍穹 [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="海伊 [SV]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            //Synthesizer V Studio
            new VoiceBank(){Reference="赤羽 [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},            
            new VoiceBank(){Reference="诗岸 [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="苍穹 [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},         
            new VoiceBank(){Reference="海伊 [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},        
            new VoiceBank(){Reference="ゲンブ [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},       
            new VoiceBank(){Reference="AiKO [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="Saki [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="琴葉 茜・葵 [SVS]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            new VoiceBank(){Reference="牧心", Profile="", Website="", Category=CategoryType.SYNTHV},           
            new VoiceBank(){Reference="星尘Minus", Profile="", Website="", Category=CategoryType.SYNTHV},            
            new VoiceBank(){Reference="小春六花 [SVS Std]", Profile="", Website="", Category=CategoryType.SYNTHV},            
            //Synthesizer V Studio AI
            new VoiceBank(){Reference="Saki [AI]", Profile="", Website="", Category=CategoryType.SYNTHV},           
            new VoiceBank(){Reference="小春六花 [AI]", Profile="", Website="", Category=CategoryType.SYNTHV},          
            //
            // AI Talk
            //
            //VOICEROID
            new VoiceBank(){Reference="月読ショウタ [VO]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="月読アイ [VO]", Profile="", Website="", Category=CategoryType.AITALK},          
            //VOICEROID+
            new VoiceBank(){Reference="吉田くん [VO+]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="弦巻マキ [VO+]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="結月ゆかり [VO+]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="東北ずん子 [VO+]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="琴葉 茜・葵 [VO+]", Profile="", Website="", Category=CategoryType.AITALK},
            //VOICEROID+ EX
            new VoiceBank(){Reference="月読ショウタ [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="月読アイ [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="弦巻マキ [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="結月ゆかり [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="東北ずん子 [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="水奈瀬コウ", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="京町セイカ [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="東北きりたん [VO+EX]", Profile="", Website="", Category=CategoryType.AITALK},          
            //VOICEROID2
            new VoiceBank(){Reference="結月ゆかり [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="琴葉 茜・葵 [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="紲星あかり [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="桜乃そら [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="東北イタコ [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="ついなちゃん [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="伊織弓鶴 [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="音街ウナ [VO2]", Profile="", Website="", Category=CategoryType.AITALK},
            //AIVOICE
            new VoiceBank(){Reference="琴葉 茜・葵 [AIV]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="伊織弓鶴 [AIV]", Profile="", Website="", Category=CategoryType.AITALK},
            //Talk licensed software
            new VoiceBank(){Reference="音街ウナ [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="galaco [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="鳴花ヒメ [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="鳴花ミコト [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="flower [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            new VoiceBank(){Reference="GUMI [Talk]", Profile="", Website="", Category=CategoryType.AITALK},
            //
            // OTHERS
            //
            new VoiceBank(){Reference="初音ミク [NT]", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="Aquestone", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="Aquestone 2", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="ALYS", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="Bones", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="Marie Ork", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="LEORA", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="Chipspeech", Profile="", Website="", Category=CategoryType.OTHERS},
            new VoiceBank(){Reference="UTAU Shareware", Profile="", Website="", Category=CategoryType.OTHERS},
            //Any new database added after the initial release of this software

        };

        /// <summary>
        /// Get private list
        /// </summary>
        public static List<VoiceBank> GetList()
        {
            return digitalBoxes;
        }

        /// <summary>
        /// Modify local product list information
        /// </summary>
        public static void ModifyList(string checkRef, string newCode, string newLink)
        {
            int found = ProductData.GetList().FindIndex(x => x.Reference == checkRef);
            ProductData.GetList()[found].ActivationCode = newCode;
            ProductData.GetList()[found].Link = newLink;
        }

        /// <summary>
        /// ExportJSON into VoCatalogue_data.ini
        /// </summary>
        public static void ExportJSON()
        {
            jsonString = JsonConvert.SerializeObject(digitalBoxes);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\VoCatalogue_data.ini", jsonString);
            Debug.Write("File written on " + AppDomain.CurrentDomain.BaseDirectory + @"\VoCatalogue_data.ini");
        }

        /// <summary>
        /// ImportJSON into VoCatalogue_data.ini
        /// </summary>
        public static void ImportJSON()
        {
            jsonString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\VoCatalogue_data.ini");
            List<VoiceBank> importData = JsonConvert.DeserializeObject<List<VoiceBank>>(jsonString);
            Debug.Write("File successfully read from " + AppDomain.CurrentDomain.BaseDirectory + @"\VoCatalogue_data.ini");
            if (importData != null)
            {
                int found = 0;
                foreach (VoiceBank v in importData)
                {
                    digitalBoxes[found].Link = v.Link;
                    digitalBoxes[found].ActivationCode = v.ActivationCode;
                    digitalBoxes[found].ActivationBool = v.ActivationBool;
                    found++;
                }

                importData.Clear();
            }
        }


        /// <summary>
        /// Settings checking before loading grids
        /// </summary>
        public static void VibeCheck()
        {
            //TODO: positions are hardcoded (as they are old banks with fixed positions), will need change if I can use
            //key-based containers
            if (Properties.Settings.Default.ai_sweetann_v2 == true) digitalBoxes[5].Profile = "img/profile/v2/sweetann_legacy.jpg";
            if (Properties.Settings.Default.ai_prima_v2 == true) digitalBoxes[8].Profile = "img/profile/v2/prima_legacy.jpg";
            if (Properties.Settings.Default.ai_sonika_v2 == true) digitalBoxes[13].Profile = "img/profile/v2/sonika_legacy.jpg";
            if (Properties.Settings.Default.ai_bigal_v2 == true) digitalBoxes[17].Profile = "img/profile/v2/bigal_legacy.jpg";
            if (Properties.Settings.Default.ai_tonio_v2 == true) digitalBoxes[19].Profile = "img/profile/v2/tonio_legacy.jpg";
            

        }



    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {

            MAIKAmodule();

            InitializeComponent();

            ProductData.ImportJSON();

            ProductData.VibeCheck();

            LoadGrids();

        }

        /// <summary>
        /// Culture/Localization language check
        /// </summary>
        private void MAIKAmodule()
        {
            //0 = English
            //1 = Spanish 
            //2 = Japanese
            string culture = "";
            switch (Properties.Settings.Default.language)
            {
                case 0:
                    culture = "en";
                    break;
                case 1:
                    culture = "es";
                    break;
                case 2:
                    culture = "ja-JP";
                    break;

            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);


        }

        /// <summary>
        /// Loads grids on their respective tabs
        /// </summary>
        public void LoadGrids()
        {
            //creation of lists to be sorted later on the loop
            List<VoiceBank> items_vocaloid = new List<VoiceBank>();
            List<VoiceBank> items_cevio = new List<VoiceBank>();
            List<VoiceBank> items_synthV = new List<VoiceBank>();
            List<VoiceBank> items_aitalk = new List<VoiceBank>();
            List<VoiceBank> items_others = new List<VoiceBank>();
            //auxiliar bank
            VoiceBank aux;
            //grid values
            int x = 0, y;

            for (int i = 0; i < ProductData.GetList().Count(); i++)
            {
                if (ProductData.GetList()[i].ActivationCode != "")
                {
                    //TODO: will need to change [i] with [key] and the loop with foreach if I can enable key-based containers
                    aux = ProductData.GetList()[i];
                    y = i % 2;



                    aux.GridRowPos = x.ToString();
                    aux.GridColPos = y.ToString();
                    //Debug.Write("Row:" + x + " Column:" + y + "\n");

                    switch (aux.Category)
                    {
                        case CategoryType.VOCALOID:
                            items_vocaloid.Add(aux);
                            break;

                        case CategoryType.CeVIO:
                            items_cevio.Add(aux);
                            break;

                        case CategoryType.SYNTHV:
                            items_synthV.Add(aux);
                            break;

                        case CategoryType.AITALK:
                            items_aitalk.Add(aux);
                            break;

                        case CategoryType.OTHERS:
                            items_others.Add(aux);
                            break;

                    }

                    //offset
                    if (y == 1)
                    {
                        x++;
                    }
                }
            }

            datacontrol_vocaloid.ItemsSource = items_vocaloid;
            datacontrol_cevio.ItemsSource = items_cevio;
            datacontrol_synthv.ItemsSource = items_synthV;
            datacontrol_aitalk.ItemsSource = items_aitalk;
            datacontrol_others.ItemsSource = items_others;
        }

        /// <summary>
        /// Open the VocaList window
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VocaList win2 = new VocaList();
            win2.Show();
            this.Close();
        }

        /// <summary>
        /// Open the VocaSettings window
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VocaSettings win3 = new VocaSettings();
            win3.ShowDialog();
        }

        /// <summary>
        /// Action done when checking "Deactivate?"
        /// </summary>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            //Grab generated item
            Border parent2 = (Border)(((FrameworkElement)((Border)((CheckBox)sender).Parent).Parent).Parent);

            //Find object linked to the generated item
            string n = ((VoiceBank)parent2.DataContext).Reference;
            int found = ProductData.GetList().FindIndex(x => x.Reference == n);

            parent2.Background.Opacity = 0.5;
            ProductData.GetList()[found].ActivationBool = true;
            ProductData.ExportJSON();
            //Debug.Write(ProductData.GetList()[found].Reference + " is now in a state of " + ProductData.GetList()[found].ActivationBool + "\n");
        }

        /// <summary>
        /// Action done when unchecking "Deactivate?"
        /// </summary>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            //Grab generated item
            Border parent2 = (Border)(((FrameworkElement)((Border)((CheckBox)sender).Parent).Parent).Parent);

            //Find object linked to the generated item
            string n = ((VoiceBank)parent2.DataContext).Reference;
            int found = ProductData.GetList().FindIndex(x => x.Reference == n);

            parent2.Background.Opacity = 1;
            ProductData.GetList()[found].ActivationBool = false;
            ProductData.ExportJSON();
            //Debug.Write(ProductData.GetList()[found].Reference + " is now in a state of " + ProductData.GetList()[found].ActivationBool + "\n");
        }

        /// <summary>
        /// Initializes the opacity of the object
        /// </summary>
        private void Border_Initialized(object sender, EventArgs e)
        {

            //Find object linked to the generated item
            string n = ((VoiceBank)((Border)sender).DataContext).Reference;
            int found = ProductData.GetList().FindIndex(x => x.Reference == n);

            //Change opacity of item
            if (ProductData.GetList()[found].ActivationBool == true)
            {
                ((Border)sender).Background.Opacity = 0.5;
            };
        }

        /// <summary>
        /// Generic navigation system
        /// </summary>
        private void GeneralRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        /// <summary>
        /// Action done when clicking the installer button
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Grab generated item
            Border parent2 = (Border)(((FrameworkElement)((Button)sender).Parent).Parent);

            //Find object linked to the generated item
            string n = ((VoiceBank)parent2.DataContext).Reference;
            int found = ProductData.GetList().FindIndex(x => x.Reference == n);

            string ActCode = ProductData.GetList()[found].ActivationCode;
            if (ActCode != "") Clipboard.SetText(ActCode);

            try { System.Diagnostics.Process.Start(ProductData.GetList()[found].Link); }
            catch (Exception)
            {
                //TEST
                MessageBox.Show("The installer file (.exe) can't be found!\nCheck your VocaList settings just in case.", "Σ(゜ロ゜;)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void SafeproofName_Initialized(object sender, EventArgs e)
        {
            //Grab generated item
            Border parent2 = (Border)(((FrameworkElement)((TextBlock)sender).Parent).Parent);

            //Find object linked to the generated item
            string n = ((VoiceBank)parent2.DataContext).Reference;
            int found = ProductData.GetList().FindIndex(x => x.Reference == n);

            string ProfilePic = ProductData.GetList()[found].Profile;
            if (ProfilePic != "") ((TextBlock)sender).Opacity = 0;
        }
    }



}
