import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/**
 * Validator that ensures the date is not before today
 */
export function minDateTodayValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }
    
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    const selectedDate = new Date(control.value);
    
    return selectedDate < today ? { minDateToday: true } : null;
  };
}

/**
 * Validator that ensures checkout date is after checkin date
 * Use this on the checkout field
 */
export function checkOutAfterCheckInValidator(checkInFieldName: string = 'checkIn'): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const form = control.parent;
    if (!form) {
      return null;
    }
    
    const checkIn = form.get(checkInFieldName)?.value;
    const checkOut = control.value;
    
    if (!checkIn || !checkOut) {
      return null;
    }
    
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);
    
    return checkOutDate <= checkInDate ? { checkOutBeforeCheckIn: true } : null;
  };
}

/**
 * Validator for phone numbers (10-15 digits)
 */
export function phoneNumberValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }
    
    const phonePattern = /^[0-9]{10,15}$/;
    return phonePattern.test(control.value) ? null : { invalidPhone: true };
  };
}

/**
 * Validator for star rating (1-5)
 */
export function starRatingValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }
    
    const rating = Number(control.value);
    if (isNaN(rating)) {
      return { invalidRating: true };
    }
    
    return rating >= 1 && rating <= 5 ? null : { invalidRating: true };
  };
}
