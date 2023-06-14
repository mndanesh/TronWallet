using HDWallet.Core;
using HDWallet.Tron;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Coinobin.Website.Tron
{
    public class WalletModel
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
    }   
    public class WalletBalanceModel
    {
        public class Data
        {
            public string tokenId { get; set; }
            public string tokenName { get; set; }
            public int tokenDecimal { get; set; }
            public string tokenAbbr { get; set; }
            public int tokenCanShow { get; set; }
            public string tokenType { get; set; }
            public string tokenLogo { get; set; }
            public bool vip { get; set; }
            public string balance { get; set; }
            public double tokenPriceInTrx { get; set; }
            public double tokenPriceInUsd { get; set; }
            public double assetInTrx { get; set; }
            public double assetInUsd { get; set; }
            public double percent { get; set; }
        }

        public class Root
        {
            public double totalAssetInTrx { get; set; }
            public List<Data> data { get; set; }
            public double totalAssetInUsd { get; set; }
        }
    }
    public static class Wallet
    {
        public static WalletModel Generate()
        {
            var words = new dotnetstandard_bip39.BIP39().GenerateMnemonic(256, dotnetstandard_bip39.BIP39Wordlist.English);
            var password = "yourpassword";
            IHDWallet<TronWallet> tronHDWallet = new TronHDWallet(words, password);
            var depositWallet0 = tronHDWallet.GetAccount(0).GetExternalWallet(0);
            var address = depositWallet0.Address;
            var privatekey = depositWallet0.PrivateKey.ToHex();
            WalletModel model = new WalletModel()
            {
                Address = address,
                PrivateKey = privatekey
            };
            return model;
        }
        public static double GetBalance(string address)
        {
            var json = new WebClient().DownloadString("https://apilist.tronscanapi.com/api/account/token_asset_overview?address=" + address.Trim());
            WalletBalanceModel.Root data = JsonConvert.DeserializeObject<WalletBalanceModel.Root>(json);
            var usdt = data.data.FirstOrDefault(x => x.tokenAbbr.ToLower() == "usdt");
            if (usdt == null)
                return 0;
            else
                return usdt.assetInUsd;
        }
    }
}
