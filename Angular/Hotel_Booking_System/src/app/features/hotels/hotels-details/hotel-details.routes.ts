import { Routes } from "@angular/router";



export const routes:Routes=[
    {path:"rooms",loadComponent:()=> import('../rooms/rooms').then(m=>m.Rooms)},
    {path:"reviews",loadComponent:()=>import("../reviews/reviews").then(m=>m.Reviews)},
    {path:"employees",loadComponent:()=>import("../employees/employees").then(m=>m.Employees)},
    {path:'',redirectTo:'rooms',pathMatch:'full'}
]  