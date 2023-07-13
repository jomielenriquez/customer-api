# Customer Information API - Backend Challenge
by Jomiel Enriquez
   - Email: enriquez.jliquigan
   - Contact Number: 09963637231

## <span style="color: #f79a8e !important"> DELETE </span> Remove CustomerById

```url
/api/RemoveCustomerById?id=<CustomerId>
```

Delete specific customer information using the customer id.

### Query Params
<table style="width:500px; border-top:solid 1px">
    <tr>
        <td style="text-align:center"><h5>id</h5></td>
        <td style="text-align:center"><h5> <_CustomerId_> </h5></td>
    </tr>
</table>

## <span style="color: #f79a8e !important"> POST </span> Create

```url
/api/CreateCustomer
```

Create new customer info

### Body raw (text)
```json
{
    "firstname":"Rachell",
    "lastname":"Cordero",
    "birthdayinepoch":"01/01/2022",
    "email":"test@mail.com"
}
```

