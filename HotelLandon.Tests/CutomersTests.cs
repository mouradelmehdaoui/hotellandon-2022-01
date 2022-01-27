using HotelLandon.Models;
using HotelLandon.Repository;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xunit;

namespace HotelLandon.Tests
{
    public class CutomersTests
    {
        private DateTime dt;
        private bool isValid;

        [Fact]
        public void Test1()
        {
            // Arrange : initialiser vos variables
            Customer customer = new()
            {
                FirstName = "Andres"
            };

            // Act : Exécuter les opérations nécessaires


            // Assert : Vérifiez que le résultat attendu est égale à celui produit
            Assert.True(customer.FirstName.Length > 3);
        }

        [Fact]
        public void FirstNameDoitAvoir3Caracteres_AvecUneLettre()
        {
            // Arrange
            Customer customer = new()
            {
                FirstName = "A"
            };

            // Act

            // Assert
            Assert.False(customer.FirstName.Length > 3);
        }

        [Fact]
        public void DateDeNaissance()
        {
            // Arrange
            Customer customer = new()
            {
                BirthDate = DateTime.Today.AddYears(-120)
            };

            // Act

            // Assert
            Assert.Throws<TropVieuxException>(() =>
            {
                // Ajouter une vérification au niveau du Customer
            });
        }

        [Fact]
        public void FirstName_Espace_TraitUnion()
        {
            // Arrange
            // Arrange : initialiser vos variables
            Customer customer = new()
            {
                FirstName = "El mehdaoui"
            };
            Customer customer2 = new()
            {
                FirstName = "El-mehdaoui"
            }; 
            Customer customer3 = new()
            {
                FirstName = "El  mehdaoui"
            }; 
            
            Customer customer4 = new()
            {
                FirstName = "El--mehdaoui"
            };

            string pattern = @"^\p{L}*[- ]?\p{L}*$";
            Regex rgx = new Regex(pattern);

            // Act : Exécuter les opérations nécessaires


            // Assert : Vérifiez que le résultat attendu est égale à celui produit
            Assert.False(!Regex.IsMatch(customer.FirstName, pattern));
            Assert.False(!Regex.IsMatch(customer2.FirstName, pattern));
            Assert.True(!Regex.IsMatch(customer3.FirstName, pattern));
            Assert.True(!Regex.IsMatch(customer4.FirstName, pattern));
        }

        [Fact]
        public void FirstName_Not_Empty()
        {
            // Arrange
            Customer customer = new()
            {
                FirstName = ""
            };

            // Act

            // Assert
            Assert.True(String.IsNullOrWhiteSpace(customer.FirstName));
        }

        [Fact]
        public void DateDeNaissance_BonFormat_DateValide()
        {
            // Arrange
            Customer customer = new()
            {
                BirthDate = new DateTime(2011, 2, 19)

         
                };


            string dtNaissance = customer.BirthDate.ToString("dd/M/yyyy");

            // Act


            //Vérifiez si la date est entrée au format jj/MM/aaaa.
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2 ])\/((19|20)\d\d))$");

            //Vérifiez si la date saisie est une date valide.
            isValid = DateTime.TryParseExact(dtNaissance, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);

            //Vérifiez si la date est entrée au format jj/MM/aaaa.
            Assert.False(!regex.IsMatch(dtNaissance));
            Assert.True(isValid);


            // Assert

        }



    }
}
