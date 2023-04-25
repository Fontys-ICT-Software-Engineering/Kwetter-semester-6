using Kweet.Models;
using KweetService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace KweetServiceTest.Services.TestHelper
{
    public static class TestHelperClass
    {
        public static List<KweetModel> GetFakeKweetsList()
        {
            return new List<KweetModel>()
            {
            new KweetModel("Test Message 1", "User1"),
            new KweetModel("Test Message 2", "User2"),
            new KweetModel("Test Message 3", "User3"),
            new KweetModel("Test Message 4", "User123")
            };
        }

        public static List<Like> GetFakeLikeList()
        {
            return new List<Like>()
            {
                new Like()
            };
        }

        public static List<ReactionKweet> GetFakeReactionList()
        {
            return new List<ReactionKweet>()
            {
                new ReactionKweet()
            };
        }
    }

}
