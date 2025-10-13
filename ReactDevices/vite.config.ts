import { defineConfig } from 'vite';
import path from 'path';

export default defineConfig({
	root: '.',
	build: {
		outDir: path.resolve(__dirname, '../Net21/WebPortal/wwwroot/comp-catalog'),
		emptyOutDir: true,
		rollupOptions: {
			input: path.resolve(__dirname, 'index.html'),
			output: {
				entryFileNames: 'index.js',
				assetFileNames: 'assets/[name][extname]'
			}
		}
	},
});

