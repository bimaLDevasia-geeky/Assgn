import { Routes } from "@angular/router";

export const routes: Routes = [
    {path:'hotel',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/hotel/hotel').then(m=>m.Hotel)},
    {path:'room',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/room/room').then(m=>m.RoomComponent)},
    {path:'roomtype',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/roomtype/roomtype').then(m=>m.Roomtype)},
    {path:'booking',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/booking/booking').then(m=>m.BookingComponent)},
    {path:'user',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/user/user').then(m=>m.User)},
    {path:'employees',loadComponent:()=>import('../../../pages/admin/admin-dashboard/pages/employees/employees').then(m=>m.Employees)},
    {path:'',redirectTo:'hotel', pathMatch:'full'}
]