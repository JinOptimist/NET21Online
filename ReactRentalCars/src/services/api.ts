import { RentalCar, CreateRentalCarRequest } from '../types/RentalCar';

const API_BASE_URL = 'https://localhost:7243';

// API service for rental cars
export class RentalCarsApi {
  private static async request<T>(endpoint: string, options?: RequestInit): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    const response = await fetch(url, {
      headers: {
        'Content-Type': 'application/json',
        ...options?.headers,
      },
      ...options,
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response.json();
  }

  // Get all rental cars
  static async getRentalCars(): Promise<RentalCar[]> {
    return this.request<RentalCar[]>('/GetRentalCars');
  }

  // Get car names only
  static async getCarsNames(): Promise<string[]> {
    return this.request<string[]>('/GetCarsNames');
  }

  // Get URLs only
  static async getUrls(): Promise<string[]> {
    return this.request<string[]>('/GetUrls');
  }

  // Create new rental car
  static async createRentalCar(car: CreateRentalCarRequest): Promise<number> {
    return this.request<number>('/createRentalCars', {
      method: 'POST',
      body: JSON.stringify(car),
    });
  }
}


