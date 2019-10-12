# import image_slicer
# image_slicer.slice('road气泡.png', 4)

import math
import os
from PIL import Image

def slice(image_path, out_name, outdir, slice_size):
    """slice an image into parts slice_size """
    img = Image.open(image_path)
    width, height = img.size
    upper = 0
    left = 0
    slices = int(math.ceil(width/slice_size))

    count = 1
    for slice in range(slices):
        #if we are at the end, set the lower bound to be the bottom of the image
        if count == slices:
            lower = width
        else:
            lower = int(count * slice_size)  

        bbox = (left, upper, lower, height)
        working_slice = img.crop(bbox)
        left += slice_size
        #save the slice
        working_slice.save(os.path.join(outdir, out_name + "." + str(count)+".sliced.png"))
        count +=1

if __name__ == '__main__':
    folder = "3.Sofa Wasted/新图"
    path = "/Users/jskyzero/workspace/unity/Minigame/Unity/Yoroshiku/Assets/Textures/Map/" + folder + "/"
    config = [
      (path, "图层 1"),
      (path, "靠背"),
      (path, "靠背 拷贝"),
      ]
 
    for (i, j) in config:
      slice(i + j + ".png", j, i, 2000)