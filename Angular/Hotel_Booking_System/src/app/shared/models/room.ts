export enum RoomStatus {
    AVAILABLE = 'Available',
    BOOKED = 'Booked',
    MAINTENANCE = 'Maintenance'
}


export interface RoomType{
  id: string;
  typeName: string;
  description: string;
  capacity: number;
}
export interface Room {
    id: string;
    hotelId: string;
    roomNumber: string;
    floor: number;
    pricePerNight: number;
    roomType: RoomType;
    status: RoomStatus;
}
