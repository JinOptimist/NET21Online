import React, { useState, useEffect } from 'react';
import { RentalCar } from './types/RentalCar';
import { RentalCarsApi } from './services/api';
import CreateRentalCarForm from './components/CreateRentalCarForm';
import RentalCarsList from './components/RentalCarsList';
import './App.css';

const App: React.FC = () => {
  const [cars, setCars] = useState<RentalCar[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Load rental cars on component mount
  useEffect(() => {
    loadRentalCars();
  }, []);

  const loadRentalCars = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const rentalCars = await RentalCarsApi.getRentalCars();
      setCars(rentalCars);
    } catch (err) {
      setError('Failed to load rental cars. Please check if the API is running.');
      console.error('Error loading rental cars:', err);
    } finally {
      setIsLoading(false);
    }
  };

  const handleCarCreated = () => {
    // Reload the list after creating a new car
    loadRentalCars();
  };

  return (
    <div className="app">
      {/* Main Content */}
      <main className="app-main">
        <div className="main-content">
          <h1 className="page-title">Add Rental car on Shop Page</h1>
          
          <CreateRentalCarForm onCarCreated={handleCarCreated} />
          
          <RentalCarsList 
            cars={cars}
            isLoading={isLoading}
            error={error}
          />
        </div>
      </main>
    </div>
  );
};

export default App;


