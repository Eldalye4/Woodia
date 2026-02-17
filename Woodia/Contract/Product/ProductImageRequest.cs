using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductImageRequest(
    string Url,
    string imagePlace);