import * as webpack from 'webpack';
import * as path from 'path';
const { InjectManifest } = require('workbox-webpack-plugin');

const appPath = path.resolve(__dirname, 'app');
const outputPath = path.resolve(__dirname, 'dist');

const config: webpack.Configuration = {
  mode: 'production',
  entry: {
    app: path.resolve(appPath, 'index.ts'),
  },
  output: {
    filename: '[name].intevent.js',
    chunkFilename: '[name].intevent.js',
    path: outputPath,
  },
  plugins: [
    new InjectManifest({
      importWorkboxFrom: 'cdn',
      swDest: path.resolve(outputPath, 'sw.intevent.js'),
      swSrc: path.resolve(appPath, 'sw.ts'),
    }),
  ],
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        loaders: ['ts-loader'],
        sideEffects: false,
      },
      {
        test: /\.(png|svg|jpg|gif)$/,
        use: ['file-loader'],
      },
      {
        test: /\.(woff|woff2|eot|ttf|otf)$/,
        use: ['file-loader'],
      },
    ]
  },
  optimization: {
    splitChunks: {
      cacheGroups: {
        vendor: {
          test: /node_modules/,
          chunks: 'all',
          name: 'vendor',
          priority: 10,
          enforce: true,
        },
      },
    },
  },
};

export default config;
