using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Canalex.AWS.BizRules;

namespace Canalex.AWS.BizRules.Unit.Tests
{
    public class LenderBizRuleTest
    {
        private LenderBizRule _lenderBizRule;

        public void SetUp()
        {
            _lenderBizRule = new LenderBizRule();
        }

        [Test]
        public void GetMyFavouriteLender_Today_Success()
        {
            //ARRANGE
            var today = DateTime.Today.ToString(CultureInfo.InvariantCulture);

            //ACT
            var result = _lenderBizRule.GetMyFavouriteLender(today);

            //ASSERT
            Assert.AreEqual($"My favourite lender on {today} is C.I.B.C", result);
        }
    }
}
