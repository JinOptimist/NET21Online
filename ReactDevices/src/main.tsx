import React from 'react';
import { createRoot } from 'react-dom/client';
import CatalogApp from './CatalogApp';

const rootElement = document.getElementById('catalog-root');
if (rootElement) {
  const root = createRoot(rootElement);
  root.render(
    <React.StrictMode>
      <CatalogApp />
    </React.StrictMode>
  );
}


