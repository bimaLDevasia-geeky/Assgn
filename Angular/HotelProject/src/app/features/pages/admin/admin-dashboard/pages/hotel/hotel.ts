import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { HotelService } from './hotel.service';
import { catchError, count, Observable } from 'rxjs';
import { HotelFormType, Hotel as HotelType } from '../../../../../../shared/types/admin.hotel.types';
import { CommonModule } from '@angular/common';
import { faEdit,faTrash,faAdd } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalComponent } from '../../../../../../shared/components/modal/modal';
import { ToastService } from '../../../../../../shared/services/toast.sercie';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-hotel',
  imports: [FontAwesomeModule, ɵInternalFormsSharedModule,ReactiveFormsModule, CommonModule, ModalComponent],
  templateUrl: './hotel.html',
  styleUrl: './hotel.scss',
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class Hotel {

  faEdit = faEdit;
  faTrash = faTrash;
  faAdd = faAdd;
  
  isEditMode = signal<boolean>(false);
  isAddMode = signal<boolean>(false);
  hotelform:FormGroup| null = null;
  selectedHotel = signal<string | null>(null);

  public hotels = signal<HotelType[]>([]);

  constructor(
    private hotelService: HotelService,
    private fb:FormBuilder,
    private router: Router,
    private toast: ToastService
  ) {
    this.loadHotels();
  }


  intialiseEditForm(hotel: HotelType) {
    this.isEditMode.set(true);
    this.selectedHotel.set(hotel.id);
    this.hotelform =this.fb.group({
      name:[hotel.name, [Validators.required, Validators.minLength(3)]],
      country:[hotel.country, [Validators.required, Validators.minLength(2)]],
      starRating:[hotel.starRating, [Validators.required, Validators.min(1), Validators.max(5)]],
      address :[hotel.address, [Validators.required, Validators.minLength(5)]],
      phoneNumber:[hotel.phoneNumber, [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)]],
      city:[hotel.city, [Validators.required, Validators.minLength(2)]],
    });
  }

  intialiseAddForm() {
    this.isEditMode.set(false);
    this.isAddMode.set(true);
    this.selectedHotel.set(null);
    this.hotelform = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      country: ['', [Validators.required, Validators.minLength(2)]],
      starRating: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
      address: ['', [Validators.required, Validators.minLength(5)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10,15}$/)]],
      city: ['', [Validators.required, Validators.minLength(2)]],
    });
  }
  cancelEdit() {
    this.isEditMode.set(false);
    this.isAddMode.set(false);
    this.selectedHotel.set(null);
    this.hotelform = null;
  }

  onSubmit() {
    if (!this.hotelform || this.hotelform.invalid) {
      this.hotelform?.markAllAsTouched();
      return;
    }

    if (this.isAddMode()) {
      this.addHotel(this.hotelform.value);
    } else if (this.isEditMode()) {
      this.editHotel(this.hotelform.value);
    }
  }

  loadHotels() {
    this.hotelService.loadHotels().pipe(
      catchError((error) => {
        console.error('Error fetching hotels:', error);
        throw error;
      })
    ).subscribe({
      next: (hotels: HotelType[]) => {
        this.hotels.set(hotels);
      }
    });
  }

  addHotel(hotelData: HotelFormType) {
    this.hotelService.addhotel(hotelData).subscribe({
      next: () => {
        this.loadHotels(); 
        this.isAddMode.set(false);
        this.hotelform = null;
        this.toast.success('Hotel added successfully');
      },
      error: (err) => {
        console.error('Error adding hotel', err);
        this.toast.error('Failed to add hotel. Please try again later.');
      }
    });
  }


  editHotel(hotelData: HotelFormType) {

    this.hotelService.edithotel(this.selectedHotel()!, hotelData).pipe(
      catchError((error) => {
        console.error('Error editing hotel:', error);
        throw error;
      })
    ).subscribe({ 
      next:(value:HotelFormType) => {
        this.loadHotels();
        this.isEditMode.set(false);
        this.isAddMode.set(false);
        this.hotelform = null;
        console.log('Hotel edited successfully',value);
        this.selectedHotel.set(null);
        this.toast.success('Hotel edited successfully');
      },
      error: (error:any) => {
        console.error('Error editing hotel:', error);
        this.toast.error('Failed to edit hotel. Please try again later.');
      }
    
    });
  }
  deleteHotel(hotel: HotelType) {
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to delete ${hotel.name}? This will also delete all associated rooms and employees.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.hotelService.deletehotel(hotel.id).pipe(
          catchError((error) => {
            console.error('Error deleting hotel:', error);
            throw error;
          })
        ).subscribe({
          next: () => {
            this.loadHotels();
            this.toast.success(`Hotel ${hotel.name} deleted successfully!`);
          },
          error: (error) => {
            console.error('Error deleting hotel:', error);
            this.toast.error('Failed to delete hotel. Please try again later.');  
          }
        });
      }
    });
  }

  viewRooms(hotelId: string) {
    this.router.navigate(['/admin/dashboard/room'], {
      queryParams: { hotelId }
    });
  }

  viewEmployees(hotelId: string) {
    this.router.navigate(['/admin/dashboard/employees'], {
      queryParams: { hotelId }
    });
  }

}
