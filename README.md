# Hotel Management

This application allows you o manage hotel room availability and reservations. It reads from files containing hotel data and booking data, then allows a user to check room availability for a specified hotel, date range, and room type.

## Example Command to Run the Program:

```bash
./hotel --hotels hotels.json --bookings bookings.json
```

If you don't specify --hotels or --bookings parameters, the default values for them are respectively hotels.json and bookings.json. 
If you want to use different date format than yyyyMMdd then you can specify another optional parameter

```bash
./hotel --hotels hotels.json --bookings bookings.json --dateFormat yyyy-MM-dd
```

## Example: `hotels.json`

```json
[
   { 
      "id": "H1",
      "name": "Hotel California",
      "roomTypes": [
         { 
            "code": "SGL",
            "description": "Single Room",
            "amenities": ["WiFi", "TV"],
            "features": ["Non-smoking"]
         },
         { 
            "code": "DBL",
            "description": "Double Room",
            "amenities": ["WiFi", "TV", "Minibar"],
            "features": ["Non-smoking", "Sea View"]
         }
      ],
      "rooms": [
         { 
            "roomType": "SGL",
            "roomId": "101"
         },
         { 
            "roomType": "SGL",
            "roomId": "102"
         },
         { 
            "roomType": "DBL",
            "roomId": "201"
         },
         { 
            "roomType": "DBL",
            "roomId": "202"
         }
      ]
   }
]
```

## Example: `bookings.json`

```json
[
   { 
      "hotelId": "H1",
      "arrival": "20240901",
      "departure": "20240903",
      "roomType": "DBL",
      "roomRate": "Prepaid"
   },
   { 
      "hotelId": "H1",
      "arrival": "20240902",
      "departure": "20240905",
      "roomType": "SGL",
      "roomRate": "Standard"
   }
]
```

Repository provides example hotels.json and bookings.json files.

The program implements the two commands described below. The program exits when a blank line is entered.

## Commands

### Availability Command

**Example Console Input:**

```plaintext
Availability(H1, 20240901-20240903, DBL)
```

**Output:**  
```plaintext
Available rooms for the specified date: 1
```

**Note:** Hotels sometimes accept overbookings, so the value can be negative to indicate this.

### Search Command

**Example Input:**

```plaintext
Search(H1, 365, SGL)
```

**Output:**  
The program returns a comma-separated list of date ranges and availability where the room is available. The `365` is the number of days to look ahead. If there is no availability, the program returns an empty line.

**Example Output:**

```plaintext
(20241101-20241103, 2), (20241203-20241210, 1)
```

Meaning that in the next 365 days, there are 2 rooms of type "SGL" in hotel "H1" available from **2024.11.01 to 2024.11.03** and 1 room available from **2024.12.03 to 2024.12.10**.
