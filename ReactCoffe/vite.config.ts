import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    strictPort: true,
    https: false,
    proxy: {
      '/api': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      }
      ,
      '/Swagger': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      },
      '/GetNameCoffe': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      },
      '/GetUrlsCoffee': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      },
      '/createCoffe': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      },
      '/Product': {
        target: 'https://localhost:7187',
        changeOrigin: true,
        secure: false,
      }
    }
  }
})

