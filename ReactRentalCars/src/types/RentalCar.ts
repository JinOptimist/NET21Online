// Rental car model interface matching the API
export interface RentalCar {
  id: number;
  name: string;
  description: string;
  url: string;
  price: number;
}

// Form data for creating new rental car
export interface CreateRentalCarRequest {
  name: string;
  description: string;
  url: string;
  price: number;
}


