using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace ZPlusGameTests
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string email = "test07@email.com";
        private string password = "Test@07";
        [SetUp]
        public async Task Setup()
        {

            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }

        private void Login()
        {
            // Navigate to ZPlusGame
            driver.Navigate().GoToUrl("https://zplusgame.com/login");

            // Wait until the page is loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the email input field and enter the email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(email); // Replace with valid credentials

            // Find the password input field and enter the password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password); // Replace with valid credentials

            // Find the login button and click it
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        }
        [Test]
        public void Register_NameAndEmailAndPassword_SuccessfulRegistration()
        {
            // Navigate to the registration page
            driver.Navigate().GoToUrl("https://zplusgame.com/register");

            string username = "user_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the name input field and enter a name
            IWebElement nameInput = driver.FindElement(By.Id("name"));
            nameInput.SendKeys("Test User");

            // Find the email input field and enter an email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(username+"@mail.com");

            // Find the password input field and enter a password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password); 

            // Find the register button and click it
            IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            registerButton.Click();


            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the user menu button and assert it
            IWebElement userMenuButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(@id, 'headlessui-menu-button')]")));
            Assert.That(userMenuButton.Displayed, Is.True, "User menu button is not displayed");
     }

        [Test]
        public void Register_AlreadyRegisteredEmailAndNameAndPassword__Failure()
        {
            // Navigate to the registration page
            driver.Navigate().GoToUrl("https://zplusgame.com/register");

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the name input field and enter a name
            IWebElement nameInput = driver.FindElement(By.Id("name"));
            nameInput.SendKeys("Test User");

            // Find the email input field and enter an email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(email);

            // Find the password input field and enter a password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password);

            IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            registerButton.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Verify that the registration failed by checking the presence of the error message
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.go3958317564")));
            Assert.Multiple(() =>
            {
                Assert.That(errorMessage.Displayed, Is.True, "Error message for already registered email is not displayed");
                Assert.That(errorMessage.Text.Contains("Email already in use."), "Error message text is incorrect");
            });
        }

        [Test]
        public void Login_EmailAndPassword_SuccessfulLogin()
        {
            // Navigate to ZPlusGame
            driver.Navigate().GoToUrl("https://zplusgame.com/login");

            // Wait until the page is loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the email input field and enter the email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(email); // Replace with valid credentials

            // Find the password input field and enter the password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password); // Replace with valid credentials

            // Find the login button and click it
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the user menu button and assert it
            IWebElement userMenuButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[contains(@id, 'headlessui-menu-button')]")));
            Assert.That(userMenuButton.Displayed, Is.True, "User menu button is not displayed");

        }

        [Test]
        public void Login_UnresteredEmailAndassword_Failure()
        {
            // Navigate to ZPlusGame
            driver.Navigate().GoToUrl("https://zplusgame.com/login");

            // Wait until the page is loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the email input field and enter an unregistered email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("test1@main.com");

            // Find the password input field and enter a valid password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password);

            // Find the login button and click it
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Verify that the login failed by checking the presence of the error message
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.go3958317564")));
            Assert.Multiple(() =>
            {
                Assert.That(errorMessage.Displayed, Is.True, "Error message for invalid email is not displayed");
                Assert.That(errorMessage.Text.Contains("Authentication failed."), "Error message text is incorrect");
            });
        }

        [Test]
        public void Login_EmailAndInvalidPassword_Failure()
        {
            // Navigate to ZPlusGame
            driver.Navigate().GoToUrl("https://zplusgame.com/login");

            // Wait until the page is loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Find the email input field and enter an  email address
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(email);

            // Find the password input field and enter an invalid password
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("invalidPass");

            // Find the login button and click it
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Verify that the login failed by checking the presence of the error message
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.go3958317564")));
            Assert.Multiple(() =>
            {
                Assert.That(errorMessage.Displayed, Is.True, "Error message for invalid email is not displayed");
                Assert.That(errorMessage.Text.Contains("Authentication failed."), "Error message text is incorrect");
            });
        }

        [Test]
        public void Logout_NA_SuccessfulLogout()
        {
            // Navigate to the page
            driver.Navigate().GoToUrl("https://zplusgame.com/");

            Login();

            // Wait until the user menu button is visible
            IWebElement userMenuButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@id, 'headlessui-menu-button')]")));

            // Click the user menu button to open the dropdown
            userMenuButton.Click();

            // Find the logout button and click it
            IWebElement logoutButton = driver.FindElement(By.CssSelector("button.block.w-full.px-4.py-2.text-sm.text-gray-700.hover\\:bg-gray-100"));
            logoutButton.Click();

            // Wait for the page to be loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            IWebElement loginButtonOnPage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.hidden.lg\\:relative.lg\\:z-10.lg\\:ml-4.lg\\:flex.lg\\:items-center a[href='/login']")));
              Assert.That(loginButtonOnPage.Displayed, Is.True, "Login button is not displayed after logout");
          
        }

        [Test]
        public void HomePage_NA_DisplayMultipleGameCards()
        {
            // Navigate to the home page
            driver.Navigate().GoToUrl("https://zplusgame.com"); // Replace with the actual URL of the home page

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the game cards
            var cardElements = driver.FindElements(By.CssSelector("a.w-52.h-44.block.bg-gray-200.border.border-gray-300.rounded-lg.overflow-hidden.shadow-md.transition-transform.duration-300.transform.hover\\:scale-105"));

            // Check if there are multiple cards
            Assert.That(cardElements.Count, Is.GreaterThan(1), "The home page does not display multiple game cards.");

            // Verify each card
            foreach (IWebElement card in cardElements)
            {
                // Verify the card contains an image
                IWebElement image = card.FindElement(By.TagName("img"));
                Assert.Multiple(() =>
                {
                    Assert.That(image.Displayed, Is.True, "Card image is not displayed.");
                    Assert.That(image.GetAttribute("alt"), Is.Not.Empty, "Card image does not have alt text.");
                });

               
            }
        }

        [Test]
        public void SearchPage_ArcherHero_DisplaySearchResults()
        {
            // Navigate to the search page
            driver.Navigate().GoToUrl("https://zplusgame.com");

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the search input and perform a search for a game
            IWebElement searchInput = driver.FindElement(By.Id("game"));
            searchInput.SendKeys("Archer Hero");
            searchInput.SendKeys(Keys.Enter);

            // Wait until the search results are displayed
            wait.Until(driver => driver.FindElement(By.XPath("//h2[contains(text(),'Search Results')]")).Displayed);

            // Locate the search results section
            IWebElement searchResultsSection = driver.FindElement(By.XPath("//h2[contains(text(),'Search Results')]"));

            // Verify that the search results section is displayed
            Assert.That(searchResultsSection.Displayed, Is.True, "Search Results section is not displayed.");

            // Locate the game cards within the search results section
            var cardElements = driver.FindElements(By.CssSelector("a.w-52.h-44.block.bg-gray-200.border.border-gray-300.rounded-lg.overflow-hidden.shadow-md.transition-transform.duration-300.transform.hover\\:scale-105"));

            // Verify that the correct number of cards are present
            Assert.That(cardElements.Count, Is.GreaterThan(0), "No game cards are displayed in the search results.");

            // Verify each game card
            foreach (IWebElement card in cardElements)
            {
                // Verify the card contains an image
                IWebElement image = card.FindElement(By.TagName("img"));
                Assert.Multiple(() =>
                {
                    Assert.That(image.Displayed, Is.True, "Card image is not displayed.");
                    Assert.That(image.GetAttribute("alt"), Is.Not.Empty, "Card image does not have alt text.");
                });
            }
        }

        [Test]
        public void RpgCategory_NA_DisplayRpgGames()
        {
            // Navigate to the page with RPG games
            driver.Navigate().GoToUrl("https://zplusgame.com/category/rpg"); 

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the RPG Games section
            IWebElement rpgSection = driver.FindElement(By.XPath("/html/body/main/section/h2"));

            // Verify that the section is displayed
            Assert.That(rpgSection.Displayed, Is.True, "RPG Games section is not displayed.");

            // Locate the game cards within the RPG Games section
            var cardElements = driver.FindElements(By.CssSelector("a.w-52.h-44.block.bg-gray-200.border.border-gray-300.rounded-lg.overflow-hidden.shadow-md.transition-transform.duration-300.transform.hover\\:scale-105"));

            // Verify that the correct number of cards are present
            Assert.That(cardElements.Count, Is.GreaterThan(0), "No RPG game cards are displayed.");

            // Verify each game card
            foreach (IWebElement card in cardElements)
            {
                // Verify the card contains an image
                IWebElement image = card.FindElement(By.TagName("img"));
                Assert.Multiple(() =>
                {
                    Assert.That(image.Displayed, Is.True, "Card image is not displayed.");
                    Assert.That(image.GetAttribute("alt"), Is.Not.Empty, "Card image does not have alt text.");
                });

                
            }
        }

        [Test]
        public void GamePage_NA_Succesfull()
        {
            // Navigate to the Archer Hero game page
            driver.Navigate().GoToUrl("https://zplusgame.com/game/archer-hero"); 

            // Verify Page Title and Header
            Assert.That(driver.Title, Is.EqualTo("Archer Hero - Play Free on Z Plus Game"));
            IWebElement header = driver.FindElement(By.CssSelector("body > main > div > div.col-span-1.md\\:col-span-8 > h1"));
            Assert.That(header.Text, Is.EqualTo("Archer Hero"));

            // Verify Game Description
            IWebElement description = driver.FindElement(By.CssSelector("body > main > div > div.col-span-1.md\\:col-span-8 > p"));
            Assert.That(description.Displayed, Is.True);
            Assert.That(description.Text, Does.Contain("In Archer Hero, we offer you the chance to step into the realm of mysticism and magic"));

            // Verify Game Embed
            IWebElement iframe = driver.FindElement(By.CssSelector("iframe[src*='archer-hero']"));
            Assert.That(iframe.Displayed, Is.True);
            Assert.That(iframe.GetAttribute("src"), Is.EqualTo("https://www.onlinegames.io/games/2023/unity/archer-hero/index.html"));

            // Verify How to Play Section
            IWebElement howToPlaySection = driver.FindElement(By.XPath("//h2[text()='How to Play']/following-sibling::ul"));
            var howToPlayItems = howToPlaySection.FindElements(By.TagName("li"));
            Assert.That(howToPlayItems.Count, Is.EqualTo(2));
            Assert.That(howToPlayItems[0].Text, Is.EqualTo("Use the WASD or arrow keys to move your character."));
            Assert.That(howToPlayItems[1].Text, Is.EqualTo("Press the spacebar to unleash your fire attack."));

            // Verify Instructions Section
            IWebElement instructionsSection = driver.FindElement(By.XPath("//h2[text()='Instructions']/following-sibling::ul"));
            var instructionsItems = instructionsSection.FindElements(By.TagName("li"));
            Assert.That(instructionsItems.Count, Is.EqualTo(10)); // Ensure all items are present
            Assert.That(instructionsItems[0].Text, Is.EqualTo("Tracking game on the vast grassy plain."));

            // Verify Rating Section
            IWebElement ratingSection = driver.FindElement(By.XPath("//h2[text()='Rating']/following-sibling::p"));
            Assert.That(ratingSection.Text, Is.EqualTo("Rating: 4.38"));

            // Verify Comments Section
            IWebElement commentsSection = driver.FindElement(By.XPath("//h2[text()='Comments']/following-sibling::p"));
            Assert.That(commentsSection.Text, Is.EqualTo("No comments yet."));

            // Verify Categories
            var categoryLinks = driver.FindElements(By.CssSelector("a.rounded-md.bg-yellow-400"));
            Assert.That(categoryLinks.Count, Is.EqualTo(3)); // Ensure there are 3 category links
            Assert.That(categoryLinks[0].Text, Is.EqualTo("Action"));
            Assert.That(categoryLinks[1].Text, Is.EqualTo("Adventure"));
            Assert.That(categoryLinks[2].Text, Is.EqualTo("RPG")        );

            // Verify Similar Games
            var similarGames = driver.FindElements(By.CssSelector("a.w-28.h-28.block"));
            Assert.That(similarGames.Count, Is.EqualTo(10)); // Ensure there are 10 similar games

            foreach (IWebElement game in similarGames)
            {
                var img = game.FindElement(By.TagName("img"));
                Assert.That(img.Displayed, Is.True);
                Assert.That(img.GetAttribute("alt"), Is.Not.Empty);
                Assert.That(img.GetAttribute("src"), Does.Contain("/_next/image?url="));
            }
        }


        [Test]
        public void AddComment_AwsomeGame_DisplaySuccessMessage()
        {
            // Navigate to the page
            driver.Navigate().GoToUrl("https://zplusgame.com/");

            Login();

            // Wait until the page is fully loaded
          wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the game link using its href attribute
            var maskedSpecialForcesLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/game/masked-special-forces']")));

            // Verify that the link is displayed
            Assert.That(maskedSpecialForcesLink.Displayed, Is.True, "Masked Special Forces link is not displayed.");

            // Click on the game link
            maskedSpecialForcesLink.Click();


            // Wait until the page is fully loaded
             wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the comment textarea and input a comment
            IWebElement commentInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("content")));
            commentInput.SendKeys("Awesome game");

            // Locate the 'Add Comment' button and click it to submit the comment
            IWebElement submitButton = driver.FindElement(By.XPath("//button[text()='Add Comment']"));
             submitButton.Click();

            // Wait for the success message to appear
            wait.Until(driver => driver.FindElement(By.XPath("//*[contains(text(),'Comment added successfully')]")).Displayed);

            // Locate the success message
             IWebElement successMessage = driver.FindElement(By.XPath("//*[contains(text(),'Comment added successfully')]"));

            // Verify that the success message is displayed
            Assert.That(successMessage.Displayed, Is.True, "Success message is not displayed after submitting the comment.");
        }

        [Test]
        public void AddComment_Ok_DisplayErrorMessage()
        {

            Login();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the game link using its href attribute
            var maskedSpecialForcesLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/game/masked-special-forces']")));

            // Verify that the link is displayed
            Assert.That(maskedSpecialForcesLink.Displayed, Is.True, "Masked Special Forces link is not displayed.");

            // Click on the game link
            maskedSpecialForcesLink.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Wait until the comment textarea is visible
            IWebElement commentInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("content")));

            // Input a short comment in the textarea
            commentInput.SendKeys("Ok");

            // Wait until the 'Add Comment' button is clickable
            IWebElement submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Add Comment']")));

            // Click the 'Add Comment' button to submit the comment
            submitButton.Click();

            // Wait for the error message to appear
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),'Please enter a comment more than 3 character.')]")));

            // Verify that the error message is displayed
            Assert.That(errorMessage.Displayed, Is.True, "Error message is not displayed when submitting a short comment.");
        }


        [Test]
        public void AddComment_WithoutLogin_DisplayLoginPrompt()
        {
            // Navigate to the page
            driver.Navigate().GoToUrl("https://zplusgame.com/");

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Locate the game link using its href attribute
            var maskedSpecialForcesLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='/game/masked-special-forces']")));

            // Verify that the link is displayed
            Assert.That(maskedSpecialForcesLink.Displayed, Is.True, "Masked Special Forces link is not displayed.");

            // Click on the game link
            maskedSpecialForcesLink.Click();

            // Wait until the page is fully loaded
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            // Wait until the comment textarea is visible
            IWebElement commentInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("content")));

            // Input a comment in the textarea
            commentInput.SendKeys("Great game!");

            // Wait until the 'Add Comment' button is clickable
            IWebElement submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Add Comment']")));

            // Click the 'Add Comment' button to submit the comment
            submitButton.Click();

            // Wait for the "Please login to comment" message to appear
            IWebElement loginPrompt = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),'Please login to comment.')]")));

            // Verify that the login prompt is displayed
            Assert.That(loginPrompt.Displayed, Is.True, "Login prompt is not displayed when attempting to comment without logging in.");
        }



    }
}
