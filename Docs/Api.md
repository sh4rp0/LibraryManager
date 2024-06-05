# LibraryManager API

- [LibraryManager API](#library-manager-api)
  - [Auth](#auth)
    - [Register](#register)
	- [Login](#login)
  - [Books](#book)
    - [Add Book](#add-book)
	- [Get Book by Id](#get-book-by-id)
	- [Update Book](#update-book)
	- [Delete Book](#delete-book)
  - [Borrowing](#borrowing)
    - [Borrow Book](#borrow-book)
	- [Return Book](#return-book)

# Auth

## Register

```
POST {{host}}/auth/register
```

### Request

```json
{
    "firstName": "Matus",
    "lastName": "Zaprazny",
    "email": "matus@zaprazny.com",
    "password": "Matus123!"
}
```

### Response

```json
{
	"id": "36419197-f9f9-426e-a8d3-1c4ed1085fe4", 
	"firstName": "Matus",
	"lastName": "Zaprazny",
	"email": "matus@zaprazny.com",
	"token":"eyJhbGciO...sw5c",
}
```
```js
200 OK
```

## Login

```
POST {{host}}/auth/login
```

### Request

```json
{
	"email": "matus@zaprazny.com",
    "password": "Matus123!"
}
```

### Response

```json
{
	"id": "36419197-f9f9-426e-a8d3-1c4ed1085fe4", 
	"firstName": "Matus",
	"lastName": "Zaprazny",
	"email": "matus@zaprazny.com",
	"token":"eyJhbGciO...sw5c",
}
```
```js
200 OK
```

# Books

## Add Book

```
POST {{host}}/books
```

### Request

```
Authorization: Bearer {{token}}
```

```json
{
    "title": "Alica v krajine zazrakov",
    "description": "Zaujimava poviedka",
    "author": "Lewis Carroll",
    "isbn": "123-456-789-X",
    "totalCopies": 26
}
```

### Response

```json
{
	"id": 3,
	"title": "Alica v krajine zazrakov",
	"description": "Zaujimava poviedka",
	"author": "Lewis Carroll",
	"isbn": "123-456-789-X",
	"totalCopies": 26,
	"createdDateTime": "2024-06-05T14:28:09.6121524",
	"updatedDateTime": "2024-06-05T14:28:09.6121929"
}
```
```js
201 Created
```

## Get Book by Id
```
GET {{host}}/books/{{id}}

```

### Request

```
Authorization: Bearer {{token}}
```

```json
body none
```

### Response

```json
{
	"id": 3,
	"title": "Alica v krajine zazrakov",
	"description": "Zaujimava poviedka",
	"author": "Lewis Carroll",
	"isbn": "123-456-789-X",
	"totalCopies": 26,
	"createdDateTime": "2024-06-05T14:28:09.6121524",
	"updatedDateTime": "2024-06-05T14:28:09.6121929"
}
```
```js
200 OK
```

## Update Book
```
PUT {{host}}/books/{{id}}
```
### Request

```
Authorization: Bearer {{token}}
```
```json
	"id": {{id}},
    "title": "Móricko na ceste do sveta Simpanzov",
    "description": "Zaujimava cesta",
    "author": "Devin Black",
    "isbn": "123-456-789-2",
    "totalCopies": 2
```

### Response

```json
{
  "id": 2,
  "title": "Móricko na ceste do sveta Simpanzov",
  "description": "Zaujimava cesta",
  "author": "Devin Black",
  "isbn": "123-456-789-2",
  "totalCopies": 2,
  "createdDateTime": "2024-06-04T16:41:19.3960293",
  "updatedDateTime": "2024-06-05T14:29:34.7208048"
}
```
```js
200 OK
```

## Delete Book
```
DELETE {{host}}/books/{{id}}
```
## Request

```
Authorization: Bearer {{token}}
```

```json
body none
```

### Response
```js
204 No Content
```

# Borrowing

## Borrow Book
```
POST {{host}}/borrowings
```
## Request

```
Authorization: Bearer {{token}}
```
```json
{
    "userId": "3b3a65fb-749b-4da8-a934-59afb91e4e5b",
    "bookId": "2",
    "maxDaysUntilReturn": 30
}
```

## Response

```json
{
	"id": 2,
	"userId": "3b3a65fb-749b-4da8-a934-59afb91e4e5b",
	"bookId": 3,
	"isReturned": false,
	"dueDate": "2024-06-06T00:00:00",
	"createdDateTime": "2024-06-05T14:45:23.9104872"
}
```
```js
201 Created
```

## Return Book
```
PATCH {{host}}/borrowings/{{id}}
```

### Request

```
Authorization: Bearer {{token}}
```
```json
{
    "id": 2,
    "isReturned": true
}
```

### Response
```json
{
	"id": 2,
	"userId": "3b3a65fb-749b-4da8-a934-59afb91e4e5b",
	"bookId": 3,
	"isReturned": true,
	"dueDate": "2024-06-06T00:00:00",
	"createdDateTime": "2024-06-05T14:45:23.9104872"
}
```
```js
200 OK
```