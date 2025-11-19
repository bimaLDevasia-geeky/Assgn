import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RoomService } from '../../../../../../shared/services/admin-room.service';
import { RoomTypeService } from '../../../../../../shared/services/roomtype.service';
import { Room as RoomModel, RoomType, CreateRoomCommand, Room, RoomStatus } from '../../../../../../shared/types/admin.types';
import { catchError } from 'rxjs';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrash, faPlus, faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '../../../../../../shared/components/modal/modal';
import { ToastService } from '../../../../../../shared/services/toast.sercie';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-room',
  imports: [ReactiveFormsModule, FontAwesomeModule, CommonModule, ModalComponent],
  templateUrl: './room.html',
  styleUrl: './room.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoomComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private roomService = inject(RoomService);
  private roomTypeService = inject(RoomTypeService);
  private toast = inject(ToastService);

  faEdit = faEdit;
  faTrash = faTrash;
  faPlus = faPlus;
  faArrowLeft = faArrowLeft;

  hotelId = signal<string>('');
  rooms = signal<Room[]>([]);
  roomTypes = signal<RoomType[]>([]);
  roomStatuses: RoomStatus[] = ['Available', 'Occupied', 'UnderMaintenance'];
  
  isEditMode = signal<boolean>(false);
  isAddMode = signal<boolean>(false);
  roomForm: FormGroup | null = null;
  selectedRoomId = signal<string | null>(null);

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const id = params['hotelId'];
      if (id) {
        this.hotelId.set(id);
        this.loadRooms();
      } else {
        this.router.navigate(['/admin/dashboard/hotel']);
      }
    });
    this.loadRoomTypes();
  }

  loadRooms(): void {
    console.log('🔵 Loading rooms for hotel:', this.hotelId());
    this.roomService.getRoomsByHotelId(this.hotelId()).pipe(
      catchError((error) => {
        console.error('Error fetching rooms:', error);
        this.toast.error('Failed to load rooms. Please try again.');
        throw error;
      })
    ).subscribe({
      next: (rooms: Room[]) => {
        console.log('✅ Rooms loaded successfully:', rooms);
        this.rooms.set(rooms);
      }
    });
  }

  loadRoomTypes(): void {
    this.roomTypeService.getAllRoomTypes().subscribe({
      next: (types) => {
        console.log('Room types loaded:', types);
        this.roomTypes.set(types);
      },
      error: (err) => console.error('Error loading room types:', err)
    });
  }

  initializeAddForm(): void {
    this.isAddMode.set(true);
    this.isEditMode.set(false);
    this.selectedRoomId.set(null);
    this.roomForm = this.fb.group({
      roomTypeId: ['', Validators.required],
      roomNumber: ['', Validators.required],
      pricePerNight: [0, [Validators.required, Validators.min(0)]],
      status: ['Available', Validators.required]
    });
  }

  initializeEditForm(room: RoomModel): void {
    this.isEditMode.set(true);
    this.isAddMode.set(false);
    this.selectedRoomId.set(room.id);
    this.roomForm = this.fb.group({
      roomTypeId: [room.roomTypeId, Validators.required],
      roomNumber: [room.roomNumber, Validators.required],
      pricePerNight: [room.pricePerNight, [Validators.required, Validators.min(0)]],
      status: [room.status, Validators.required]
    });
  }

  cancelEdit(): void {
    this.isEditMode.set(false);
    this.isAddMode.set(false);
    this.selectedRoomId.set(null);
    this.roomForm = null;
  }

  onSubmit(): void {
    if (!this.roomForm || this.roomForm.invalid) {
      this.roomForm?.markAllAsTouched();
      return;
    }

    if (this.isAddMode()) {
      this.addRoom(this.roomForm.value);
    } else if (this.isEditMode()) {
      this.editRoom(this.roomForm.value);
    }
  }

  addRoom(roomData: Room): void {
    const command: CreateRoomCommand = {
      ...roomData,
      hotelId: this.hotelId()
    };
    console.log('🔵 Creating room:', command);
    
    this.roomService.createRoom(command).subscribe({
      next: (response) => {
        console.log('✅ Room created successfully:', response);
        this.loadRooms();
        this.toast.success('Room added successfully!');
        this.cancelEdit();
      },
      error: (err) => {
        console.error('❌ Error adding room:', err);
        this.toast.error('Failed to add room. Please try again.');
      }
    });
  }

  editRoom(roomData: Room): void {
    const command = {
      ...roomData,
      hotelId: this.hotelId(),
      id: this.selectedRoomId()!
    };
    console.log('🔵 Updating room:', command);
    
    this.roomService.updateRoom(this.selectedRoomId()!, command).subscribe({
      next: (response) => {
        console.log('✅ Room updated successfully:', response);
        this.loadRooms();
        this.toast.success('Room updated successfully!');
        this.cancelEdit();
      },
      error: (err) => {
        console.error('❌ Error editing room:', err);
        this.toast.error('Failed to update room. Please try again.');
      }
    });
  }

  deleteRoom(room: Room): void {
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to delete Room ${room.roomNumber}? This action cannot be undone.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        console.log('🔵 Deleting room:', room.id);
        this.roomService.deleteRoom(room.id).subscribe({
          next: () => {
            console.log('✅ Room deleted successfully');
            this.loadRooms();
            this.toast.success(`Room ${room.roomNumber} deleted successfully!`);
          },
          error: (err) => {
            console.error('❌ Error deleting room:', err);
            this.toast.error('Failed to delete room. Please try again.');
          }
        });
      }
    });
  }

  getRoomTypeName(roomTypeId: string): string {
    const roomType = this.roomTypes().find(rt => rt.id === roomTypeId);
    return roomType ? roomType.name : 'Unknown';
  }

  getStatusLabel(status: RoomStatus): string {
    switch (status) {
      case 'Available':
        return 'Available';
      case 'Occupied':
        return 'Occupied';
      case 'UnderMaintenance':
        return 'Under Maintenance';
      default:
        return status;
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/dashboard/hotel']);
  }
}
