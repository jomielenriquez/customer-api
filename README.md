# Customer Information API - Backend Challenge
by Jomiel Enriquez

![](./svg/DELETE_CustomerById.svg)
### Query Params
<table style="width:500px; border-top:solid 1px;">
    <tr>
        <td style="text-align:center"><h5>id</h5></td>
        <td style="text-align:center"><h5> <_CustomerId_> </h5></td>
    </tr>
</table>



![](./svg/POST_CreateCustomer.svg)
### Body raw (text)
```json
{
    "firstname":"Rachell",
    "lastname":"Cordero",
    "birthdayinepoch":"01/01/2022",
    "email":"test@mail.com"
}
```



![](./svg/GET_GetCustomerById.svg)
### Query Params
<table style="width:500px; border-top:solid 1px">
    <tr>
        <td style="text-align:center"><h5>id</h5></td>
        <td style="text-align:center"><h5> <_CustomerId_> </h5></td>
    </tr>
</table>



## <span style="color: #f79a8e !important"> GET </span> GetAllCustomers

```url
/api/GetAllCustomers
```

Get all customer information.



## <span style="color: #f79a8e !important"> POST </span> UpdateCustomer

```url
/api/UpdateCustomer?id=<CustomerId>
```

Get specific customer information.

### Query Params
<table style="width:500px; border-top:solid 1px">
    <tr>
        <td style="text-align:center"><h5>id</h5></td>
        <td style="text-align:center"><h5> <_CustomerId_> </h5></td>
    </tr>
</table>

### Body raw (text)
```json
{
    "firstname":"Rachell",
    "lastname":"Cordero",
    "birthdayinepoch":"01/01/2022",
    "email":"test@mail.com"
}
```
