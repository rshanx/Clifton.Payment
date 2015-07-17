﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clifton.Payment;

namespace Clifton.Payment.Tests {
    public struct ExampleCard {
        public const string validVisa = "4111 1111 1111 1111";
        public const string validMasterCard = "5111 1111 1111 1118";
        public const string validAmericanExpress = "3111 1111 1111 117";
        public const string validDiners = "3000 0000 0000 04";
        public const string validDiscover = "6111 1111 1111 1116";
        public const string invalidAmericanExpress = "3111 1111 1111 116";
    }

    [TestClass]
    public class GetCardTypeTests {
        [TestMethod]
        public void getTypeValidDiners() {
            CreditCardType type = CreditCard.GetCardType(ExampleCard.validDiners);
            Assert.AreEqual(CreditCardType.Diners, type);
        }

        [TestMethod]
        public void getTypeValidAmericanExpress() {
            CreditCardType type = CreditCard.GetCardType(ExampleCard.validAmericanExpress);
            Assert.AreEqual(CreditCardType.AmericanExpress, type);
        }

        [TestMethod]
        public void getTypeValidVisa() {
            CreditCardType type = CreditCard.GetCardType(ExampleCard.validVisa);
            Assert.AreEqual(CreditCardType.Visa, type);
        }

        [TestMethod]
        public void getTypeValidMasterCard() {
            CreditCardType type = CreditCard.GetCardType(ExampleCard.validMasterCard);
            Assert.AreEqual(CreditCardType.MasterCard, type);
        }

        [TestMethod]
        public void getTypeValidDiscover() {
            CreditCardType type = CreditCard.GetCardType(ExampleCard.validDiscover);
            Assert.AreEqual(CreditCardType.Discover, type);
        }

        [TestMethod]
        public void getTypeTrimsWhitespaceBeforeProcessing() {
            CreditCardType type = CreditCard.GetCardType(string.Format("\r\n\r\n\t {0}\t\t\t\n", ExampleCard.validAmericanExpress));
            Assert.AreEqual(CreditCardType.AmericanExpress, type);
        }

        [TestMethod]
        public void getTypeInvalidNull() {
            CreditCardType type = CreditCard.GetCardType(null);
            Assert.AreEqual(CreditCardType.Invalid, type);
        }

        [TestMethod]
        public void getTypeInvalidWhitespace() {
            CreditCardType type = CreditCard.GetCardType("\t\n             ");
            Assert.AreEqual(CreditCardType.Invalid, type);
        }
    }

    [TestClass]
    public class HasValidLuhnChecksumTests {
        [TestMethod]
        public void luhnValidAmericanExpress() {
            bool result = CreditCard.HasValidLuhnChecksum(ExampleCard.validAmericanExpress);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void luhnInvalidAmericanExpress() {
            bool result = CreditCard.HasValidLuhnChecksum(ExampleCard.invalidAmericanExpress);
            Assert.IsFalse(result);
        }
    }
}