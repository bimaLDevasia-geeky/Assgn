import { Injectable, signal } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class SidebarService {

    isEditing = signal<boolean>(false);

    changeEditing(current: boolean): void {
        this.isEditing.set(!current);
    }
}