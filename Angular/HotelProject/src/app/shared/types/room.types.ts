export interface RoomType{
    id: string;
    capacity: number;
    description: string;
    typeName: string;
}


export interface Room {
  id: string;
  hotelId: string;
  roomNumber: string;
  roomType: RoomType;
  pricePerNight: number;
  status: 'Available' | 'Occupied' | 'Maintenance';
  imageUrl?: string;
}
