# Domain Models

## Menu

```csharp
class Menu{
    Menu Create();
    void AddDinner(Dinner dinner);
    void RemoveDinner(Dinner dinner);
    void UpdateSection(MenuSection section);
}
```

```json
{
  "id": "00000-000-000",
  "name": "Menu Name",
  "description": "a menu with yummy food",
  "averageRating": 4.5,
  "sections": [
    {
      "id": "0000 0000 0000",
      "name": "appetizers",
      "description": "Starters",
      "items": [
        {
          "id": "1234567890",
          "name": "Buffalo Wings",
          "description": "Deep fried pickles",
          "price": 6
        }
      ]
    }
  ],
  "createdDateTime": "2021-01-01T12:34:56",
  "updatedDateTime": "2021-01-01T12:34:56",
  "hostId": "0000 0000 0000",
  "dinnerIds": ["0000 0000 0000"],
  "menuReviewIds": ["0000 0000 0000"]
}
```
