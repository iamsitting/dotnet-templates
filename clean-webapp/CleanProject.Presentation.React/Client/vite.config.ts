import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    base: "/_content/CleanProject.Presentation.React",
    build: {
        outDir: "../wwwroot",
        emptyOutDir: true,
        manifest: 'vite/manifest.json',
        rollupOptions: {
            input: {
                index: "./src/main.tsx"
            },
            output: {
                assetFileNames: 'css/[name]-[hash][extname]',
                chunkFileNames: 'js/[name]-[hash].js',
                entryFileNames: 'js/[name]-[hash].js',
            },
        }
    }
})