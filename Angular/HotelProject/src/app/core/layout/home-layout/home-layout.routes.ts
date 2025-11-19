import { Routes } from "@angular/router";
import { userGuard } from "../../auth/guards/user.guard";



export const routes: Routes = [
    
    {path:'home', loadComponent: () => import('../../../features/pages/home/home').then(m => m.Home)},
    {path:'hotel',loadComponent:()=>import('../../../features/pages/hotels/hotels').then(m=>m.Hotels)},
    {path:'hotel-details', loadComponent:()=>import('../../../features/pages/hotel-details/hotel-details').then(m=>m.HotelDetails)},
    {path:'booking', loadComponent:()=>import('../../../features/pages/booking/booking').then(m=>m.BookingPage), canActivate: [userGuard]},
    {path:'payment-success', loadComponent:()=>import('../../../features/pages/payment-success/payment-success').then(m=>m.PaymentSuccess),canActivate: [userGuard]},
    {path:'payment-failure', loadComponent:()=>import('../../../features/pages/payment-failure/payment-failure').then(m=>m.PaymentFailure),canActivate: [userGuard]},
    {path:'profile', loadComponent:()=>import('../../../features/pages/profile/profile').then(m=>m.Profile), canActivate: [userGuard]},
    {path:"", redirectTo:'home', pathMatch:'full' }
];