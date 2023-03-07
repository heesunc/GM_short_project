using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

namespace Ad
{
    public class GoogleAdsManager : MonoBehaviour
    {
        private RewardedAd rewardedAd;
        GameManager manager;
        public Text GetMoney;
    
        // 보상형 광고 단위 ID
        private string rewardedAdUnitId = "ca-app-pub-3115045377477281/4539879882";
    
        // 앱 ID
        private string appId = "5186561";
    
        public void Start()
        {
            // Google AdMob SDK 초기화
            MobileAds.Initialize(initStatus => { });
    
            // 보상형 광고 로드
            LoadRewardedAd();

            manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    
        public void LoadRewardedAd()
        {
            // 보상형 광고 생성
            rewardedAd = new RewardedAd(rewardedAdUnitId);
    
            // 광고 로드 이벤트 처리
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
    
            // 광고 보상 완료 이벤트 처리
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    
            // 광고 요청
            AdRequest request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
        }
    
        // 보상형 광고 로드 완료 이벤트 처리
        public void HandleRewardedAdLoaded(object sender, System.EventArgs args)
        {
            Debug.Log("Rewarded Ad Loaded");
        }
    
        // 보상형 광고 로드 실패 이벤트 처리
        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("Rewarded ad failed to load: " + args.LoadAdError);
            // LoadAdError : 광고 로드에 실패한 경우 해당 속성을 통해 실패 원인에 대한 정보를 가져옴
        }
        
        // 광고 표시
        public void ShowAd()
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
            else
            {
                Debug.Log("Rewarded Ad is not ready yet.");
            }
        }
    
        // 광고 보상 완료 이벤트 처리
        public void HandleUserEarnedReward(object sender, Reward args)
        {
            Debug.Log("User Earned Reward: " + args.Type + " " + args.Amount);
            // 광고 보상 처리 코드를 여기에 작성합니다.
            GetMoney.text="획득한 돈 갯수 : "+manager.keyCount*2;
            Time.timeScale = 0; // 광고가 끝나도 시간은 흐르지 않음
        }
    }    
}
