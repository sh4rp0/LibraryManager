# LibraryManager API

- [LibraryManager API](#library-manager-api)
  - [Auth](#auth)	

## Login

```
POST {{host}}/auth/login
```


### Login Request

```json
{
	"email": "matus@zaprazny.com",
    "password": "Matus123!"
}
```

```js
200 OK
```

#### Login Response

```json
{
	"id": "36419197-f9f9-426e-a8d3-1c4ed1085fe4", 
	"firstName": "Matus",
	"lastName": "Zaprazny",
	"email": "matus@zaprazny.com",
	"token":"eyJhbGciO...sw5c",
}
```