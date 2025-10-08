/** @type {import('tailwindcss').Config} */
export default {
  content: [
    './index.html',
    './src/**/*.{ts,tsx}',
  ],
  theme: {
    extend: {
      colors: {
        coffee: {
          50: '#f7f4f1',
          100: '#efe9e3',
          200: '#dfd3c7',
          300: '#c7ad98',
          400: '#a9826a',
          500: '#8b5e3c',
          600: '#6f482e',
          700: '#573a26',
          800: '#3b271a',
          900: '#21160f'
        }
      }
    },
  },
  plugins: [],
}

