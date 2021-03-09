using UnityEngine;
using GoogleMobileAds.Api;

namespace AD
{
    public class AdvertService : MonoBehaviour
    {
        private const string BANNER_ID = "ca-app-pub-7461319891178211/5574955186";
        private const string GAME_OVER_AD_ID = "ca-app-pub-7461319891178211/1799361704";

        private void Awake()
        {
            InitBanner();
            // InitAd();
        }

        private void InitAd()
        {
            InterstitialAd ad = new InterstitialAd(GAME_OVER_AD_ID);
            AdRequest request = new AdRequest.Builder().Build();
            ad.LoadAd(request);
            
            if(ad.IsLoaded())
                ad.Show();
        }

        private  void InitBanner()
        {
            BannerView banner = new BannerView(BANNER_ID, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            banner.LoadAd(request);
        }
    }
}
