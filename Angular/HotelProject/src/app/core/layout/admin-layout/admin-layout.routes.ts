import { Routes } from "@angular/router";
import { authGuard } from "../../auth/guards/auth.guard";


export const routes: Routes = [
    {
        path:"dashboard",
        loadComponent:()=>import('../../../features/pages/admin/admin-dashboard/admin-dashboard').then(m=>m.AdminDashboard),
        loadChildren:()=>import('../../../features/pages/admin/admin-dashboard/admin-dashboard.routes').then(m=>m.routes),  
        canActivate: [authGuard]  // Requires authentication
    },
    {path:'',redirectTo:'dashboard', pathMatch:'full'}
];