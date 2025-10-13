import React, { useState } from 'react';
import { CreateRentalCarRequest } from '../types/RentalCar';
import { RentalCarsApi } from '../services/api';
import './CreateRentalCarForm.css';

interface CreateRentalCarFormProps {
  onCarCreated: () => void;
}

const CreateRentalCarForm: React.FC<CreateRentalCarFormProps> = ({ onCarCreated }) => {
  const [formData, setFormData] = useState<CreateRentalCarRequest>({
    name: '',
    description: '',
    url: '',
    price: 0,
  });
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'price' ? parseInt(value) || 0 : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    try {
      await RentalCarsApi.createRentalCar(formData);
      setFormData({ name: '', description: '', url: '', price: 0 });
      onCarCreated();
    } catch (err) {
      setError('Failed to create rental car. Please try again.');
      console.error('Error creating rental car:', err);
    } finally {
      setIsLoading(false);
    }
  };

    return (
        <div className="create-rental-car-form">
            <h2>Panel for creation rental car</h2>
            <form onSubmit={handleSubmit} className="form">
                <div className="form-group">
                    <label htmlFor="name">Car Name:</label>
                    <input
                        type="text"
                        id="name"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        required
                        className="form-input"
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        name="description"
                        value={formData.description}
                        onChange={handleInputChange}
                        required
                        className="form-textarea"
                        rows={3}
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="url">Image URL:</label>
                    <input
                        type="url"
                        id="url"
                        name="url"
                        value={formData.url}
                        onChange={handleInputChange}
                        required
                        className="form-input"
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="price">Price per day ($):</label>
                    <input
                        type="number"
                        id="price"
                        name="price"
                        value={formData.price}
                        onChange={handleInputChange}
                        required
                        min="0"
                        className="form-input"
                    />
                </div>

                {error && <div className="error-message">{error}</div>}

                <button
                    type="submit"
                    disabled={isLoading}
                    className="submit-button"
                >
                    {isLoading ? 'Creating...' : 'Add Rental Car'}
                </button>
            </form>
        </div>
    );
};

export default CreateRentalCarForm;


