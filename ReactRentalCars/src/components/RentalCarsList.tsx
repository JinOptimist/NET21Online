import React from 'react';
import { RentalCar } from '../types/RentalCar';
import './RentalCarsList.css';

interface RentalCarsListProps {
  cars: RentalCar[];
  isLoading: boolean;
  error: string | null;
}

const RentalCarsList: React.FC<RentalCarsListProps> = ({ cars, isLoading, error }) => {
  if (isLoading) {
    return (
      <div className="rental-cars-list">
        <h2>Panel with all created rental cars</h2>
        <div className="loading">Loading rental cars...</div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="rental-cars-list">
        <h2>Panel with all created rental cars</h2>
        <div className="error">Error: {error}</div>
      </div>
    );
  }

  if (cars.length === 0) {
    return (
      <div className="rental-cars-list">
        <h2>Panel with all created rental cars</h2>
        <div className="no-cars">No rental cars available. Create your first car!</div>
      </div>
    );
  }

  return (
    <div className="rental-cars-list">
      <h2>Panel with all created rental cars</h2>
      <div className="cars-grid">
        {cars.map((car) => (
          <div key={car.id || car.name} className="car-card">
            <div className="car-image">
              {car.url ? (
                <img 
                  src={car.url} 
                  alt={car.name}
                  onError={(e) => {
                    const target = e.target as HTMLImageElement;
                    target.style.display = 'none';
                    target.nextElementSibling?.classList.remove('hidden');
                  }}
                />
              ) : null}
              <div className={`placeholder-image ${car.url ? 'hidden' : ''}`}>
                <span>No Image</span>
              </div>
            </div>
            <div className="car-info">
              <h3 className="car-name">{car.name}</h3>
              <p className="car-description">{car.description}</p>
              <div className="car-price">${car.price}/day</div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RentalCarsList;


