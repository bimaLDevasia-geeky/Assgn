import { Routes } from '@angular/router';


export const routes: Routes = [
    {path:"home",loadComponent: () => import('./features/home/home').then(m => m.Home)},
    {path:"customers",loadComponent: () => import('./features/customers/customers-list/customers-list').then(m => m.Customers)},
    {path:"customers/:customerid",loadComponent: () => import('./features/customers/customer-details/customer-details').then(m => m.CustomerDetails)},
    {path:"hotels",loadComponent:()=> import('./features/hotels/hotels-list/hotels-list').then(m => m.HotelsList)},
    {path:"hotels/:hotelid",loadComponent:()=> import('./features/hotels/hotels-details/hotels-details').then(m => m.HotelsDetails),
        loadChildren: () => import('./features/hotels/hotels-details/hotel-details.routes').then(m => m.routes)
    },

    {path:"",redirectTo:"home",pathMatch:"full"}
];
