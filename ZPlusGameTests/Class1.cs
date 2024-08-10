import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import static org.mockito.Mockito.*;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class ZPlusGameTests
{

    private WebDriver driver;
    private WebDriverWait wait;

    @BeforeEach
    public void setup()
    {
        driver = new FirefoxDriver();
        wait = new WebDriverWait(driver, 100);
    }

    @AfterEach
    public void teardown()
    {
        if (driver != null)
        {
            driver.quit();
        }
    }

    @Test
    public void login_EmailAndPassword_SuccessfulLogin()
    {
        driver.navigate().to("https://zplusgame.com/login");

        wait.until(webDriver-> ((org.openqa.selenium.JavascriptExecutor)webDriver)
                .executeScript("return document.readyState").equals("complete"));

        WebElement emailInput = driver.findElement(By.id("email"));
        emailInput.sendKeys("test07@email.com"); // Replace with valid credentials

        WebElement passwordInput = driver.findElement(By.id("password"));
        passwordInput.sendKeys("Test@07"); // Replace with valid credentials

        WebElement loginButton = driver.findElement(By.cssSelector("button[type='submit']"));
        loginButton.click();

        wait.until(webDriver-> ((org.openqa.selenium.JavascriptExecutor)webDriver)
                .executeScript("return document.readyState").equals("complete"));

        WebElement userMenuButton = wait.until(ExpectedConditions.elementToBeClickable(
                By.xpath("//button[contains(@id, 'headlessui-menu-button')]")));

        assertTrue(userMenuButton.isDisplayed(), "User menu button is not displayed");
    }
}
