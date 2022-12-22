from sys import exit
from tensorflow.keras.datasets import mnist
from tensorflow.keras.layers import Input, Dense, LeakyReLU, Dropout
from tensorflow.keras.models import Model
from tensorflow.keras.preprocessing import image
from matplotlib import pyplot as plt
# Для очистки области вывода в IPython, Jupyter, Colab
from IPython import display
import numpy as np
plt_epoch = not False
epochs = 10
(x_trn, y_trn), (x_tst, y_tst) = mnist.load_data()
x_trn = x_trn / 255
x_tst = x_tst / 255
len_tst = len(y_tst)
x_trn = x_trn.reshape(len(y_trn), 784)
x_tst = x_tst.reshape(len_tst, 784)
# Случайное изображение элемента обучающего множества
i = np.random.randint(len(y_trn))
img = x_trn[i].reshape(28, 28)
plt.imshow(img)
plt.show()
arr = image.img_to_array(img) 

def one_plt(img):
    plt.figure(figsize = (2, 2))
    plt.imshow(img, cmap = 'gray')
    plt.axis('off')
    plt.show()

one_plt(img)

def one_part(units, x):
    x = Dense(units)(x)
    x = LeakyReLU()(x)
    return Dropout(0.25)(x)

latent_size = 32 # Размер латентного пространста
inp = Input(shape = (784))
x = one_part(512, inp)
x = one_part(256, x)
x = one_part(128, x)
x = one_part(64, x)
x = Dense(latent_size)(x)
encoded = LeakyReLU()(x)
x = one_part(64, encoded)
x = one_part(128, x)
x = one_part(256, x)
x = one_part(512, x)
decoded = Dense(784, activation = 'sigmoid')(x)
model = Model(inputs = inp, outputs = decoded)
model.compile('adam', loss = 'binary_crossentropy') # nadam
# model.summary()

def some_plts(imgs):
    fig, axs = plt.subplots(4, 4)
    k = -1
    for i in range(4):
        for j in range(4):
            k += 1
            img = imgs[k].reshape(28, 28)
            axs[i, j].imshow(img, cmap = 'gray')
            axs[i, j].axis('off')
    plt.subplots_adjust(wspace = 1, hspace = 0)
    plt.show()


for epoch in range(epochs):
    print('epoch:', epoch + 1)
    model.fit(x = x_trn, y = x_trn)
    # Выводим, если работаем в IPython, Jupyter или Colab
    if plt_epoch and epoch > 0 and epoch % 5 == 0:
        display.clear_output()
        arr_idx = np.random.randint(0, len_tst, 16) # class 'numpy.ndarray'
        imgs_for_test = x_tst[arr_idx].reshape(16, 784) # class 'numpy.ndarray'
        some_plts(imgs_for_test)
        imgs_pedicted = model.predict(imgs_for_test)
        some_plts(imgs_pedicted) # imgs_pedicted.shape = (16, 784)
# Прогноз из шума
img = np.random.uniform(0, 1, 16 * 784).reshape(16, 784)
imgs_pedicted = model.predict(img)
some_plts(imgs_pedicted)
exit = input()