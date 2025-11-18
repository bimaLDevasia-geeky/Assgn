// Room Status Enum
export type RoomStatus = 'Available' | 'Occupied' | 'UnderMaintenance';

// Booking Status Enum
export type BookingStatus = 'Pending' | 'Confirmed' | 'Cancelled' | 'Completed';

// Room Response
export interface Room {
  id: string;
  hotelId: string;
  roomTypeId: string;
  roomNumber: string;
  pricePerNight: number;
  status: RoomStatus;
  isAvailable: boolean;
  roomType: RoomType; // Optional, included in some responses
}

// Create/Update Room Command
export interface CreateRoomCommand {
  hotelId: string;
  roomTypeId: string;
  roomNumber: string;
  pricePerNight: number;
  status: RoomStatus;
}

export interface UpdateRoomCommand extends CreateRoomCommand {
  id: string;
}

// RoomType Response
export interface RoomType {
  id: string;
  name: string;
  description: string;
  capacity: number;
  typeName: string;
}

// Create/Update RoomType Command
export interface CreateRoomTypeCommand {
  name: string;
  description: string;
}

export interface UpdateRoomTypeCommand extends CreateRoomTypeCommand {
  id: string;
}

// Customer Response
export interface Customer {
  id: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  idProofNumber: string;
  passwordHash:string;
}

// Create/Update Customer Command
export interface CreateCustomerCommand {
  fullName: string;
  email: string;
  phoneNumber: string;
  idProofNumber: string;
  passwordHash:string;
}

export interface UpdateCustomerCommand extends CreateCustomerCommand {
  id: string;
}

// Booking Response
export interface Booking {
  id: string;
  customerId: string;
  roomId: string;
  checkInDate: string; // ISO 8601 format
  checkOutDate: string; // ISO 8601 format
  status: BookingStatus;
  customer?: Customer; // Optional, included in some responses
  room?: Room; // Optional, included in some responses
}

// Create Booking Command
export interface CreateBookingCommand {
  customerId: string;
  roomId: string;
  checkInDate: string;
  checkOutDate: string;
  totalAmount:number;
}

// Update Booking Command
export interface UpdateBookingCommand {
  
  status: BookingStatus;
}


export interface Employee{
  id: string;
  hotelId: string;
  fullName: string;
  email: string;
  role: string;
}


export interface Customer{

  id: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  idProofNumber: string;
  passwordHash: string;
}  //"id": "d17af45c-0875-48fb-72a9-08de25bbf1b5",
//     "fullName": "Bimal",
//     "email": "shuttudu@gmail.com",
//     "phoneNumber": "875928752",
//     "idProofNumber": "4857934879",
//     "passwordHash": "$2a$11$7K6Vi86gNzYefaQE5czfJej0JajdxHq5zCQ0Sbj4duc73fxulQpCq",
