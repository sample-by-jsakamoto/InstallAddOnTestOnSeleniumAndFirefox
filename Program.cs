using OpenQA.Selenium.Firefox;

// 👇 DON'T DO THIS.
//    The below commented code will cause tht following exception:
//    "System.ArgumentException: Preference xpinstall.signatures.required may not be overridden: frozen value=False, requested value=False"
//    You must use FirefoxOptions.SetPreference() instead.
// var profile = new FirefoxProfile();
// profile.SetPreference("xpinstall.signatures.required", false);

// 👇 AND ALSO, DON'T DO THIS.
//    This code is no longer working.
//    You must use FirefoxDriver.InstallAddOnFromFile() instead.
// profile.AddExtension(...);


// ⚠️ If you want to install an unsigned add-on, 
//    you must have installed the "DEVELOPER EDITION" of Firefox,
//    and pass the FirefoxOptions with a setting preference like the following code.
//    Otherwise, you will see the error below:
//    "Could not install add-on: ....xpi: ERROR_SIGNEDSTATE_REQUIRED: The addon must be signed and isn't."
//    (If you want to install a signed add-on, you don't need to pass the FirefoxOptions, but the code below doesn't have any bad side effects even if you pass it.)
var option = new FirefoxOptions();
option.SetPreference("xpinstall.signatures.required", false);

using var driver = new FirefoxDriver(option);

// In this sample project, the unsigned add-on is generated into the output folder such as "bin/Debug/net6.0", 
// whenever this project is built. (The build script is the iside of the "InstallAddOnTestOnSeleniumAndFirefox.csproj" file.)
var xpiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "borderify.xpi");

// This sample project will install the add-on described above by the below code.
driver.InstallAddOnFromFile(xpiPath);

// You will see that the "moziila.org" page has a red border due to the effect of the "borderify.xpi" add-on.
// (https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Your_first_WebExtension)
driver.Navigate().GoToUrl("https://developer.mozilla.org/");

Console.WriteLine("Press the Enter key to exit.");
while (Console.ReadKey(intercept: true).Key != ConsoleKey.Enter) ;
driver.Quit();
Console.WriteLine("Complete.");