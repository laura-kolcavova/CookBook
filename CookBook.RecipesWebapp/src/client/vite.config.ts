import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3015,
  },
  resolve: {
    alias: {
      "~": path.resolve(__dirname, "src"),
    },
  },
  define: {
    global: "globalThis",
  },
});
