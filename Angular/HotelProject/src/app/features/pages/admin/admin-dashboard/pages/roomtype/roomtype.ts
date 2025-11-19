import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RoomTypeService } from '../../../../../../shared/services/roomtype.service';
import { RoomType, CreateRoomTypeCommand } from '../../../../../../shared/types/admin.types';
import { catchError } from 'rxjs';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrash, faPlus } from '@fortawesome/free-solid-svg-icons';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '../../../../../../shared/components/modal/modal';
import { ToastService } from '../../../../../../shared/services/toast.sercie';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roomtype',
  imports: [ReactiveFormsModule, FontAwesomeModule, CommonModule, ModalComponent],
  templateUrl: './roomtype.html',
  styleUrl: './roomtype.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Roomtype implements OnInit {
  private fb = inject(FormBuilder);
  private roomTypeService = inject(RoomTypeService);
  private toast = inject(ToastService);

  faEdit = faEdit;
  faTrash = faTrash;
  faPlus = faPlus;

  roomTypes = signal<RoomType[]>([]);
  isEditMode = signal<boolean>(false);
  isAddMode = signal<boolean>(false);
  roomTypeForm: FormGroup | null = null;
  selectedRoomTypeId = signal<string | null>(null);

  ngOnInit(): void {
    this.loadRoomTypes();
  }

  loadRoomTypes(): void {
    this.roomTypeService.getAllRoomTypes().pipe(
      catchError((error) => {
        console.error('Error fetching room types:', error);
        this.toast.error('Failed to load room types. Please try again.');
        throw error;
      })
    ).subscribe({
      next: (types: RoomType[]) => {
        console.log('✅ Room types loaded successfully:', types);
        this.roomTypes.set(types);
      }
    });
  }

  initializeAddForm(): void {
    this.isAddMode.set(true);
    this.isEditMode.set(false);
    this.selectedRoomTypeId.set(null);
    this.roomTypeForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  initializeEditForm(roomType: RoomType): void {
    this.isEditMode.set(true);
    this.isAddMode.set(false);
    this.selectedRoomTypeId.set(roomType.id);
    this.roomTypeForm = this.fb.group({
      name: [roomType.name, [Validators.required, Validators.minLength(2)]],
      description: [roomType.description, [Validators.required, Validators.minLength(10)]]
    });
  }

  cancelEdit(): void {
    this.isEditMode.set(false);
    this.isAddMode.set(false);
    this.selectedRoomTypeId.set(null);
    this.roomTypeForm = null;
  }

  onSubmit(): void {
    if (!this.roomTypeForm || this.roomTypeForm.invalid) {
      this.roomTypeForm?.markAllAsTouched();
      return;
    }

    if (this.isAddMode()) {
      this.addRoomType(this.roomTypeForm.value);
    } else if (this.isEditMode()) {
      this.editRoomType(this.roomTypeForm.value);
    }
  }

  addRoomType(data: CreateRoomTypeCommand): void {
    console.log('🔵 Creating room type:', data);
    this.roomTypeService.createRoomType(data).subscribe({
      next: (response) => {
        console.log('✅ Room type created successfully:', response);
        this.loadRoomTypes();
        this.toast.success('Room type added successfully!');
        this.cancelEdit();
      },
      error: (err) => {
        console.error('❌ Error adding room type:', err);
        this.toast.error('Failed to add room type. Please try again.');
      }
    });
  }

  editRoomType(data: CreateRoomTypeCommand): void {
    const command = {
      ...data,
      id: this.selectedRoomTypeId()!
    };
    console.log('🔵 Updating room type:', command);
    
    this.roomTypeService.updateRoomType(this.selectedRoomTypeId()!, command).subscribe({
      next: (response) => {
        console.log('✅ Room type updated successfully:', response);
        this.loadRoomTypes();
        this.toast.success('Room type updated successfully!');
        this.cancelEdit();
      },
      error: (err) => {
        console.error('❌ Error editing room type:', err);
        this.toast.error('Failed to update room type. Please try again.');
      }
    });
  }

  deleteRoomType(roomType: RoomType): void {
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to delete ${roomType.name}? This action cannot be undone.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        console.log('🔵 Deleting room type:', roomType.id);
        this.roomTypeService.deleteRoomType(roomType.id).subscribe({
          next: () => {
            console.log('✅ Room type deleted successfully');
            this.loadRoomTypes();
            this.toast.success(`Room type ${roomType.name} deleted successfully!`);
          },
          error: (err) => {
            console.error('❌ Error deleting room type:', err);
            this.toast.error('Failed to delete room type. Please try again.');
          }
        });
      }
    });
  }
}
