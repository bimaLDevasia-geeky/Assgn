export interface Hotel {
    id:string;
    name:string;
    address:string;
    city:string;
    country:string;
    phoneNumber:string;
    starRating:number | null;

}

export interface HotelFormType{
    name:string;
    address:string;
    city:string;
    country:string;
    phoneNumber:string;
    starRating:number | null;
}