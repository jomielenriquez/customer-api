# Customer Information API - Backend Challenge
by Jomiel Enriquez

![](./svg/DELETE_CustomerById.svg)

 - ### Query Params
    | Key | Value |
    | ------------- | ------------- |
    | id  | {CustomerId}  |


![](./svg/POST_CreateCustomer.svg)
 - ### Body raw (text)
    ```json
    {
        "firstname":"Jomiel",
        "lastname":"Enriquez",
        "birthdayinepoch":"01/01/2022",
        "email":"test@mail.com"
    }
    ```



![](./svg/GET_GetCustomerById.svg)
 - ### Query Params
    | Key | Value |
    | ------------- | ------------- |
    | id  | {CustomerId}  |


![](./svg/GET_GetAllCustomers.svg)

![](./svg/POST_UpdateCustomer.svg)

### Query Params
 - ### Query Params
    | Key | Value |
    | ------------- | ------------- |
    | id  | {CustomerId}  |

 - ### Body raw (text)
    ```json
    {
        "lastname":"Enriquez",
        "birthdayinepoch":"01/01/2022",
        "email":"test@mail.com"
    }
    ```
