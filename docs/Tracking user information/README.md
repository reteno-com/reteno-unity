# Tracking User Information

## External User ID

Add your custom External User IDs within `Reteno` using the following method:

```csharp
using System.Collections.Generic;
using RetenoSdk;

public class YourClass
{
    public void SetExternalUserId()
    {
        // RetenoSDK should be initialized before set user attributes
        RetenoSdk.Initialize(accessKey, externalId);
        
        User user = new User();

        RetenoSdk.SetUserAttributes(user);
    }
}

public class User
{
    public UserAttributes UserAttributes { get; set; }
    public List<string> SubscriptionKeys { get; set; }
    public List<string> GroupNamesInclude { get; set; }
    public List<string> GroupNamesExclude { get; set; }
}

public class UserAttributes
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LanguageCode { get; set; }
    public string TimeZone { get; set; }
    public Address Address { get; set; }
    public List<Field> Fields { get; set; }
}

public class Address
{
    public string Region { get; set; }
    public string Town { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
}

public class Field
{
    public string Key { get; set; }
    public string Value { get; set; }
}
```

## User Attributes
User attributes are attributes you define to describe segments of your user base, such as language preference or geographic location. Add user attributes like phone, email, etc., using the following method:
```csharp
using System.Collections.Generic;
using RetenoSdk;

public class YourClass
{
    public void SetUserAttributes()
    {
        string externalUserId = "USER_ID";
        User user = new User
        {
            UserAttributes = new UserAttributes
            {
                Phone = "PHONE_NUMBER",
                Email = "EMAIL_ADDRESS",
                FirstName = "FIRST_NAME",
                LastName = "LAST_NAME",
                LanguageCode = "LANGUAGE_CODE",
                TimeZone = "TIME_ZONE",
                Address = new Address
                {
                    Region = "REGION",
                    Town = "TOWN",
                    Address = "ADDRESS",
                    Postcode = "POSTCODE"
                },
                Fields = new List<Field>
                {
                    new Field { Key = "KEY1", Value = "VALUE1" },
                    new Field { Key = "KEY2", Value = "VALUE2" }
                }
            },
            SubscriptionKeys = new List<string> { "SUBSCRIPTION_KEY1", "SUBSCRIPTION_KEY2" },
            GroupNamesInclude = new List<string> { "GROUP_NAME1", "GROUP_NAME2" },
            GroupNamesExclude = new List<string> { "GROUP_NAME3", "GROUP_NAME4" }
        };

        RetenoSdk.SetUserAttributes(new SetUserAttributesPayload
        {
            ExternalUserId = externalUserId,
            User = user
        });
    }
}

public class User
{
    public UserAttributes UserAttributes { get; set; }
    public List<string> SubscriptionKeys { get; set; }
    public List<string> GroupNamesInclude { get; set; }
    public List<string> GroupNamesExclude { get; set; }
}

public class UserAttributes
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LanguageCode { get; set; }
    public string TimeZone { get; set; }
    public Address Address { get; set; }
    public List<Field> Fields { get; set; }
}

public class Address
{
    public string Region { get; set; }
    public string Town { get; set; }
    public string Address { get; set; }
    public string Postcode { get; set; }
}

public class Field
{
    public string Key { get; set; }
    public string Value { get; set; }
}
```
Note:
- LanguageCode: The language data should be in RFC 5646 format. The primary language subtag in [ISO 639-1](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes) format is required. Example: de-AT.
- TimeZone: Choose an item from the [TZ database](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones). Example: Europe/Kyiv.