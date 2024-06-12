import { resolve } from 'path'
module.exports = {
  build: {
    rollupOptions: {
      input: {
        index: resolve(__dirname, './src/index.js')
      },
      output: {
        assetFileNames: 'css/[name]-[hash][extname]',
        chunkFileNames: 'js/[name]-[hash].js',
        entryFileNames: 'js/[name]-[hash].js'
      }
    },
    sourcemap: true,
    outDir: '../wwwroot',
    manifest: true,
    emptyOutDir: true,
  }
}
